﻿namespace EmployeeManagementAPI.Models;

public class Employee
{
    int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Salary { get; set; }
    public Department Department { get; set; }
}