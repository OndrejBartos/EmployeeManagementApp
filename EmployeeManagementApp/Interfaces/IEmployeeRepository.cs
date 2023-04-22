using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Interfaces;

public interface IEmployeeRepository
{
    public ICollection<Employee> GetEmployees();
    public Employee? GetEmployee(int id);
    public bool CreateEmployee(Employee employee);
    public bool UpdateEmployee(Employee employee);
    public bool DeleteEmployee(int id);
    public bool EmployeeExists(int id);
}
