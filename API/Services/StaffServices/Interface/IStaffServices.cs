using API.Contract.Staff;

namespace API.Services.StaffServices.interfaces;

public interface IStaffServices
{
      Task<List<GetStaff>> GetStaffs(CancellationToken cancellationToken);
      Task<GetStaff> GetStaff(Guid id, CancellationToken cancellationToken);
      Task<Guid> UpdateStaffPassword(Guid id, string password, CancellationToken cancellationToken);
      Task<Guid> UpdateStaff(Guid id, AddStaff StaffData, CancellationToken cancellationToken);
      Task DeleteStaff(Guid id, CancellationToken cancellationToken);
}
