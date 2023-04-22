using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Interfaces;

public interface IDepartmentRepository
{
    public ICollection<Department> GetDepartments();
    public Department? GetDepartment(int id);
    public bool CreateDepartment(Department department);
    public bool UpdateDepartment(Department department);
    public bool DeleteDepartment(int id);
    public bool DepartmentExists(int id);
}
