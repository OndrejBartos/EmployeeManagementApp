using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DataContext context) : base(context) { }

    public async Task<bool> Create(int departmentId, Employee employee)
    {
        var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);

        if (department is null)
            return false;

        // Copy over department object and call base class create method.
        employee.Department = department;
        return await Create(employee);
    }

    public async Task<ICollection<Employee>> GetEmployeesFromDepartment(int departmentId)
    {
        var employees = _context.Employees.Where(e => e.Department.Id == departmentId).ToArray();
        return employees;
    }

    public async Task<bool> Update(int departmentId, Employee employee)
    {
        var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);

        if (department is null)
            return false;

        // Copy over department object and call base class update method.
        employee.Department = department;
        return await Update(employee);
    }
}
