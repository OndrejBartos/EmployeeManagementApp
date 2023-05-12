using AutoMapper;
using EmployeeManagementAPI.Dto;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetEmployees()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employees = _mapper.Map<ICollection<EmployeeDto>>(await _repository.GetAll());

        return Ok(employees);
    }

    [HttpGet("/api/departments/{id}/employees")]
    [ProducesResponseType(200, Type = typeof(ICollection<EmployeeDto>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetEmployeesFromDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employees = _mapper.Map<ICollection<EmployeeDto>>(await _repository.GetEmployeesFromDepartment(id));

        return Ok(employees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(EmployeeDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetEmployee(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employee = _mapper.Map<EmployeeDto>(await _repository.GetById(id));

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpGet("{id}/department")]
    [ProducesResponseType(200, Type = typeof(Department))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetEmployeeDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employee = await _repository.GetById(id, e => e.Department);
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
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employee)
    {
        if (!ModelState.IsValid || employee is null)
            return BadRequest(ModelState);

        var exists = await _repository.Exists(employee.Id);
        if (exists)
        {
            return Conflict($"Employee with ID {employee.Id} already exists.");
        }

        var created = await _repository.Create(employee.DepartmentId, _mapper.Map<Employee>(employee));
        if (!created)
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
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employee)
    {
        if (!ModelState.IsValid || employee is null)
            return BadRequest(ModelState);

        var exists = await _repository.Exists(employee.Id);
        if (!exists)
        {
            return Conflict($"Employee with ID {employee.Id} doesn't exists.");
        }

        var updated = await _repository.Update(employee.DepartmentId, _mapper.Map<Employee>(employee));
        if (!updated)
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
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _repository.Exists(id);
        if (!exists)
        {
            return Conflict($"Employee with ID {id} doesn't exists.");
        }

        var deleted = await _repository.Delete(id);
        if (!deleted)
        {
            ModelState.AddModelError("", "Something went wrong while deleting employee.");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}
