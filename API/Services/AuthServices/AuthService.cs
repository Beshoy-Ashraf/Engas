using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Configurations;
using API.Contract.User.Request;
using API.Contract.User.Response;
using API.Core.Enums;
using API.Data.Models.Staff;
using API.Data.Models.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly AppDBContext _db;
    private readonly JwtConfig _jwtConfig;

    public AuthService(AppDBContext db, IOptions<JwtConfig> jwtConfig)
    {
        _db = db;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<TokenResponse> StaffRegisterAsync(UserRegistrationRequest registerRequest, CancellationToken ct)
    {
        if (await _db.Staffs.AnyAsync(u => u.UserName == registerRequest.UserName && u.DeletedDate == null, ct))
            throw new ArgumentException("This UserName already exists");

        if (await _db.Staffs.AnyAsync(u => u.SSN == registerRequest.SSN && u.DeletedDate == null, ct))
            throw new ArgumentException("This SSN already exists");

        var staff = new Staff
        {
            Name = $"{registerRequest.FirstName} {registerRequest.LastName}",
            UserName = registerRequest.UserName,
            Password = registerRequest.Password,
            SSN = registerRequest.SSN,
            Phone = registerRequest.Phone,
            Role = registerRequest.UserType == UserEnum.Admin ? UserEnum.Admin : UserEnum.Staff,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        await _db.Staffs.AddAsync(staff, ct);
        await _db.SaveChangesAsync(ct);

        var token = GenerateStaffJwtToken(staff);

        return new TokenResponse
        {
            Token = token,
            RefreshToken = string.Empty,
            UserId = staff.Id.ToString()
        };
    }
    public async Task<TokenResponse> StoreRegisterAsync(StoreRegistrationRequest registerRequest, CancellationToken ct)
    {
        if (await _db.Stores.AnyAsync(u => u.Code == registerRequest.Code && u.DeletedDate == null, ct))
            throw new ArgumentException("This UserName already exists");


        var store = new Store
        {
            Name = registerRequest.Name,
            Code = registerRequest.Code,
            Password = registerRequest.Password,
            City = registerRequest.City,
            Phone = registerRequest.Phone,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        await _db.Stores.AddAsync(store, ct);
        await _db.SaveChangesAsync(ct);

        var token = GenerateStoreJwtToken(store);

        return new TokenResponse
        {
            Token = token,
            RefreshToken = string.Empty,
            UserId = store.Id.ToString()
        };
    }

    public async Task<TokenResponse> StaffLoginAsync(UserLoginRequest loginRequest, CancellationToken ct)
    {
        var staff = await _db.Staffs
            .FirstOrDefaultAsync(u => u.UserName == loginRequest.UserName && u.DeletedDate == null, ct)
            ?? throw new UnauthorizedAccessException("Invalid username or password.");

        if (staff.Password != loginRequest.Password)
            throw new UnauthorizedAccessException("Invalid password.");

        var token = GenerateStaffJwtToken(staff);

        return new TokenResponse
        {
            Token = token,
            RefreshToken = string.Empty,
            UserId = staff.Id.ToString()
        };
    }
    public async Task<TokenResponse> StoreLoginAsync(StoreLoginRequest loginRequest, CancellationToken ct)
    {
        var store = await _db.Stores
            .FirstOrDefaultAsync(u => u.Code == loginRequest.StoreCode && u.DeletedDate == null, ct)
            ?? throw new UnauthorizedAccessException("Invalid StoreCode or password.");

        if (store.Password != loginRequest.Password)
            throw new UnauthorizedAccessException("Invalid password.");

        var token = GenerateStoreJwtToken(store);

        return new TokenResponse
        {
            Token = token,
            RefreshToken = string.Empty,
            UserId = store.Id.ToString()
        };
    }

    private string GenerateStaffJwtToken(Staff staff)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var claims = new List<Claim>
        {
            new("Id", staff.Id.ToString()),
            new(ClaimTypes.Name, staff.UserName),
            new(ClaimTypes.Role, staff.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private string GenerateStoreJwtToken(Store store)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var claims = new List<Claim>
        {
            new("Id", store.Id.ToString()),
            new(ClaimTypes.PostalCode, store.Code),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

