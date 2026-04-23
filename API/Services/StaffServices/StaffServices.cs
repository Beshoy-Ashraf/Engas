using API.Contract.Staff;
using API.Data.Models.Staff;
using Microsoft.EntityFrameworkCore;

namespace API.Services.StaffServices;

public class StaffServices(AppDBContext dbContext, IServiceProvider serviceProvider, ILogger<StaffServices> logger)
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly IServiceProvider _serviceProvider = serviceProvider;
      private readonly ILogger<StaffServices> _logger = logger;

      public async Task<Guid> AddStaff(AddStaff newStaff, CancellationToken cancellationToken)
      {
            var Staff = new Staff
            {
                  Name = newStaff.Name,
                  Password = newStaff.Password,
                  UserName = newStaff.UserName,
                  Phone = newStaff.Phone,
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow,
            };
            await _dbContext.Staffs.AddAsync(Staff, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Staff.Id;

      }
      public async Task<Guid> UpdateStaff(Guid id, AddStaff StaffData, CancellationToken cancellationToken)
      {

            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            Staff.Name = StaffData.Name;
            Staff.Password = StaffData.Password;
            Staff.UserName = StaffData.UserName;
            Staff.Phone = StaffData.Phone;
            Staff.UpdatedDate = DateTime.UtcNow;
            _dbContext.Staffs.Update(Staff);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Staff.Id;

      }
      public async Task<Guid> UpdateStaffPassword(Guid id, String password, CancellationToken cancellationToken)
      {

            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            Staff.Password = password;
            Staff.UpdatedDate = DateTime.UtcNow;
            _dbContext.Staffs.Update(Staff);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Staff.Id;

      }
      public async Task<GetStaff> GetStaff(Guid id, CancellationToken cancellationToken)
      {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            var StaffData = new GetStaff
            {
                  Id = Staff.Id,
                  Name = Staff.Name,
                  UserName = Staff.UserName,
                  Phone = Staff.Phone,
            };
            return StaffData;
      }
      public async Task<List<GetStaff>> GetStaffs(CancellationToken cancellationToken)
      {
            var Staffs = await _dbContext.Staffs
                  .Where(x => x.DeletedDate == default(DateTime))
                  .ToListAsync(cancellationToken);

            var StaffData = Staffs.Select(Staff => new GetStaff
            {
                  Id = Staff.Id,
                  Name = Staff.Name,
                  Phone = Staff.Phone,
                  UserName = Staff.UserName,

            }).ToList();

            return StaffData;
      }
      public async Task DeleteStaff(Guid id, CancellationToken cancellationToken)
      {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            Staff.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
      }
}
