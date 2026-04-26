using API.Contract.Staff;
using API.Data.Models.Staff;
using API.Services.StaffServices.interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.StaffServices;

public class StaffServices(AppDBContext dbContext, ILogger<StaffServices> logger) : IStaffServices
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly ILogger<StaffServices> _logger = logger;

      public async Task<Guid> UpdateStaff(Guid id, AddStaff StaffData, CancellationToken cancellationToken)
      {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            Staff.Name = $"{StaffData.FirstName} {StaffData.LastName}";
            Staff.Password = StaffData.Password;
            Staff.UserName = StaffData.UserName;
            Staff.Phone = StaffData.Phone;
            Staff.SSN = StaffData.SSN;
            Staff.UpdatedDate = DateTime.UtcNow;
            _dbContext.Staffs.Update(Staff);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Staff.Id;
      }

      public async Task<Guid> UpdateStaffPassword(Guid id, String password, CancellationToken cancellationToken)
      {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            Staff.Password = password;
            Staff.UpdatedDate = DateTime.UtcNow;
            _dbContext.Staffs.Update(Staff);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Staff.Id;
      }

      public async Task<GetStaff> GetStaff(Guid id, CancellationToken cancellationToken)
      {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            var StaffData = new GetStaff
            {
                  Id = Staff.Id,
                  Name = Staff.Name,
                  UserName = Staff.UserName,
                  Phone = Staff.Phone,
                  SSN = Staff.SSN,
            };
            return StaffData;
      }

      public async Task<List<GetStaff>> GetStaffs(CancellationToken cancellationToken)
      {
            var Staffs = await _dbContext.Staffs
                  .Where(x => x.DeletedDate == null)
                  .ToListAsync(cancellationToken);

            var StaffData = Staffs.Select(Staff => new GetStaff
            {
                  Id = Staff.Id,
                  Name = Staff.Name,
                  Phone = Staff.Phone,
                  UserName = Staff.UserName,
                  SSN = Staff.SSN,


            }).ToList();

            return StaffData;
      }

      public async Task DeleteStaff(Guid id, CancellationToken cancellationToken)
      {
            var Staff = await _dbContext.Staffs.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Staff with ID {id} was not found.");
            Staff.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
      }
}

