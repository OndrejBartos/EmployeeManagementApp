using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(DataContext context) : base(context) { }
}
