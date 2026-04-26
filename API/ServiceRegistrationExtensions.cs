using API.Services.AuthServices;
using API.Services.ItemServices;
using API.Services.ItemServices.Interface;
using API.Services.StaffServices;
using API.Services.StaffServices.interfaces;
using API.Services.StoreServices;
using API.Services.StoreServices.interfaces;
using API.Services.StoreStockService;
using API.Services.StoreStockService.Interface;

namespace Engas
{
      public static class ServiceRegistrationExtensions
      {
            public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
            {
                  services.AddScoped<IAuthService, AuthService>();
                  services.AddScoped<IStaffServices, StaffServices>();
                  services.AddScoped<IStoreServices, StoreServices>();
                  services.AddScoped<IItemInterface, ItemServices>();
                  services.AddScoped<IStoreStockService, StoreStockService>();

                  return services;
            }

      }
}
