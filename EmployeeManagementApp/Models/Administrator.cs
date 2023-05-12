using EmployeeManagementAPI.Interfaces;

namespace EmployeeManagementAPI.Models;

public class Administrator : IModel
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
