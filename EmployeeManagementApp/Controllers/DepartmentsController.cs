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
    public async Task<IActionResult> GetDepartments()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var departments = await _repository.GetAll();

        return Ok(departments);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(Department))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var department = await _repository.GetById(id);

        if (department is null)
            return NotFound();

        return Ok(department);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateDepartment([FromBody] Department department)
    {
        if (!ModelState.IsValid || department is null)
            return BadRequest(ModelState);

        var exists = await _repository.Exists(department.Id);
        if (exists)
        {
            return Conflict($"Department with ID {department.Id} already exists.");
        }

        var created = await _repository.Create(department);
        if (!created)
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
    public async Task<IActionResult> UpdateDepartment([FromBody] Department department)
    {
        if (!ModelState.IsValid || department is null)
            return BadRequest(ModelState);

        var exists = await _repository.Exists(department.Id);
        if (!exists)
        {
            return Conflict($"Department with ID {department.Id} doesn't exists.");
        }

        var updated = await _repository.Update(department);
        if (!updated)
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
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _repository.Exists(id);
        if (!exists)
        {
            return Conflict($"Department with ID {id} doesn't exists.");
        }

        var deleted = await _repository.Delete(id);
        if (!deleted)
        {
            ModelState.AddModelError("", "Something went wrong while deleting department.");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}
