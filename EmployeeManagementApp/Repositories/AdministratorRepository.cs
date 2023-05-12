using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories;

public class AdministratorRepository : BaseRepository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(DataContext context) : base(context) { }

    public async Task<bool> Exists(string username, string password)
    {
        return await _context.Administrators.AnyAsync(a => a.Username == username && a.Password == password);
    }
}
