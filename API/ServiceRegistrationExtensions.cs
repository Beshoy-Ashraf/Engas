using API.Services.AuthServices;
using API.Services.StaffServices;
using API.Services.StaffServices.interfaces;
using API.Services.StoreServices;
using API.Services.StoreServices.interfaces;

namespace Engas
{
      public static class ServiceRegistrationExtensions
      {
            public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
            {
                  services.AddScoped<IAuthService, AuthService>();
                  services.AddScoped<IStaffServices, StaffServices>();
                  services.AddScoped<IStoreServices, StoreServices>();

                  return services;
            }

      }
}
