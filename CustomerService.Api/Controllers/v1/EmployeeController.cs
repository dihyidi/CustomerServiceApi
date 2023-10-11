using CustomerService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers.v1;

[ApiController]
[Route("api/v1/employees")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpGet("{id:int}/locations")]
    public async Task<IActionResult> GetAllLocationsByEmployee(int id)
    {
        var locations = await employeeService.GetAllBusinessLocationsByEmployeeId(id);
        return StatusCode(StatusCodes.Status200OK, locations);
    }

    [HttpPost("{id:int}/locations/add")]
    public async Task<IActionResult> AddLocationToEmployee(int id, [FromBody] int locationId)
    {
        await employeeService.AddBusinessLocationToEmployee(id, locationId);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpPost("{id:int}/locations/{locationId:int}/delete")]
    public async Task<IActionResult> RemoveLocationFromEmployee(int id, int locationId)
    {
        await employeeService.RemoveBusinessLocationFromEmployee(id, locationId);
        return StatusCode(StatusCodes.Status202Accepted);
    }
}