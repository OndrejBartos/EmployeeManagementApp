using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Interfaces;

public interface IEmployeeRepository : IBaseRepository<Employee> 
{
    public Task<bool> Create(int departmentId, Employee employee);
    public Task<bool> Update(int departmentId, Employee employee);
    public Task<ICollection<Employee>> GetEmployeesFromDepartment(int departmentId);
}
