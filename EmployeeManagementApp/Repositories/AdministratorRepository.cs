using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories;

public class AdministratorRepository : BaseRepository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(DataContext context) : base(context) { }

    public bool Exists(string username, string password)
    {
        return _context.Administrators.Any(a => a.Username == username && a.Password == password);
    }
}
