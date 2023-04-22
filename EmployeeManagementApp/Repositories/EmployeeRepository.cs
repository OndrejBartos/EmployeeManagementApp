using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DataContext context) : base(context) { }
}
