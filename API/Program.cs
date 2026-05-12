using System.Text;
using API.Configurations;
using Engas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using NpgsqlTypes;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("ConnectionStrings:DefaultConnection is missing/empty. Configure it or set ConnectionStrings__DefaultConnection.");
}

// Log resolved DB host/database (sanitized: no password)
// Note: Use Console logging to avoid compile-time dependency on specific ILogger APIs.
try
{
    var csb = new NpgsqlConnectionStringBuilder(connectionString);
    Console.WriteLine(
        $"[DbConfig] Resolved DefaultConnection: Host={csb.Host}, Port={csb.Port}, Database={csb.Database}, SslMode={csb.SslMode}");

    if (string.Equals(csb.Host, "localhost", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(csb.Host, "127.0.0.1", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(csb.Host, "::1", StringComparison.OrdinalIgnoreCase))
    {
        throw new InvalidOperationException(
            $"DefaultConnection points to local Postgres ({csb.Host}:{csb.Port}). Ensure Postgres is running and reachable, or remove the env override ConnectionStrings__DefaultConnection.");
    }
}
catch (InvalidOperationException)
{
    throw;
}
catch (Exception ex)
{
    // If parsing fails, don't block startup; connection attempt will fail with Npgsql.
    Console.WriteLine($"[DbConfig] Failed to parse DefaultConnection string for host/database logging: {ex.Message}");
}

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorCodesToAdd: null);
        }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "JWT Authorization header using the Bearer scheme.",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.RegisterBusinessServices();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>()!;
if (string.IsNullOrWhiteSpace(jwtConfig.Secret))
{
    throw new InvalidOperationException("JwtConfig:Secret is missing or empty. Check production appsettings/environment variables.");
}
var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/health", () => "Healthy");
app.MapGet("/", () => "ENGAS API RUNNING");

app.Run();
