using AutoMapper;
using EmployeeManagementAPI.Dto;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentRepository _repository;

    public DepartmentsController(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Department>))]
    [ProducesResponseType(400)]
    public IActionResult GetDepartments()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var departments = _repository.GetAll();

        return Ok(departments);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(Department))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var department = _repository.GetById(id);

        if (department is null)
            return NotFound();

        return Ok(department);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult CreateDepartment([FromBody] Department department)
    {
        if (!ModelState.IsValid || department is null)
            return BadRequest(ModelState);

        if (_repository.Exists(department.Id))
        {
            return Conflict($"Department with ID {department.Id} already exists.");
        }

        if (!_repository.Create(department))
        {
            ModelState.AddModelError("", "Something went wrong while creating department.");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created department.");
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult UpdateDepartment([FromBody] Department department)
    {
        if (!ModelState.IsValid || department is null)
            return BadRequest(ModelState);

        if (!_repository.Exists(department.Id))
        {
            return Conflict($"Department with ID {department.Id} doesn't exists.");
        }

        if (!_repository.Update(department))
        {
            ModelState.AddModelError("", "Something went wrong while updating department.");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult DeleteDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_repository.Exists(id))
        {
            return Conflict($"Department with ID {id} doesn't exists.");
        }

        if (!_repository.Delete(id))
        {
            ModelState.AddModelError("", "Something went wrong while deleting department.");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}
