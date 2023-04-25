using EmployeeManagementAPI.Interfaces;

namespace EmployeeManagementAPI.Models;

public class Employee : IModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int Salary { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
}