using AutoMapper;
using EmployeeManagementAPI.Dto;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<EmployeeDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetEmployees()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employees = _mapper.Map<ICollection<EmployeeDto>>(_repository.GetAll());

        return Ok(employees);
    }

    [HttpGet("/api/Departments/{id}/employees")]
    [ProducesResponseType(200, Type = typeof(ICollection<EmployeeDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetEmployeesFromDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employees = _mapper.Map<ICollection<EmployeeDto>>(_repository.GetEmployeesFromDepartment(id));

        return Ok(employees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(EmployeeDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetEmployee(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employee = _mapper.Map<EmployeeDto>(_repository.GetById(id));

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpGet("{id}/department")]
    [ProducesResponseType(200, Type = typeof(Department))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetEmployeeDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employee = _repository.GetById(id, e => e.Department);
        var department = employee?.Department;

        if (department is null)
            return NotFound();

        return Ok(department);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult CreateEmployee([FromBody] EmployeeDto employee)
    {
        if (!ModelState.IsValid || employee is null)
            return BadRequest(ModelState);

        if (_repository.Exists(employee.Id))
        {
            return Conflict($"Employee with ID {employee.Id} already exists.");
        }

        if (!_repository.Create(employee.DepartmentId, _mapper.Map<Employee>(employee)))
        {
            ModelState.AddModelError("", "Something went wrong while creating employee.");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created employee.");
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult UpdateEmployee([FromBody] EmployeeDto employee)
    {
        if (!ModelState.IsValid || employee is null)
            return BadRequest(ModelState);

        if (!_repository.Exists(employee.Id))
        {
            return Conflict($"Employee with ID {employee.Id} doesn't exists.");
        }

        if (!_repository.Update(employee.DepartmentId, _mapper.Map<Employee>(employee)))
        {
            ModelState.AddModelError("", "Something went wrong while updating employee.");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult DeleteEmployee(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_repository.Exists(id))
        {
            return Conflict($"Employee with ID {id} doesn't exists.");
        }

        if (!_repository.Delete(id))
        {
            ModelState.AddModelError("", "Something went wrong while deleting employee.");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}
