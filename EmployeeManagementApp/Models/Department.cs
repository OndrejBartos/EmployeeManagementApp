using EmployeeManagementAPI.Interfaces;

namespace EmployeeManagementAPI.Models;

public class Department : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Budget { get; set; }
}
