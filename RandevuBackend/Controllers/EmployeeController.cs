using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RandevuBackend.Models;
using RandevuBackend.Services;

namespace RandevuBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EmployeeController:ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllEmployeeAsync();
        return Ok(employees);
    }

    [HttpPost("AddEmployee")]
    public async Task<IActionResult> AddEmployee(Employee employee)
    {
        await _employeeService.AddEmployee(employee);
        return Ok(employee);
    }
    
}