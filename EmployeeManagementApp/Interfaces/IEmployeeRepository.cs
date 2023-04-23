using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Interfaces;

public interface IEmployeeRepository : IBaseRepository<Employee> 
{
    bool Create(int departmentId, Employee employee);
    bool Update(int departmentId, Employee employee);
}
