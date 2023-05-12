using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Interfaces;

public interface IAdministratorRepository : IBaseRepository<Administrator>
{
    public Task<bool> Exists(string username, string password);
}
