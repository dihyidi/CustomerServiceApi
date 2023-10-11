using CustomerService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers.v1;

[ApiController]
[Route("api/v1/locations")]
[Authorize]
public class BusinessLocationController : ControllerBase
{
    private readonly IBusinessLocationService locationService;

    public BusinessLocationController(IBusinessLocationService locationService)
    {
        this.locationService = locationService;
    }

    [HttpGet("{id:int}/employees")]
    public async Task<IActionResult> GetBusinessLocationEmployees(int id)
    {
        var employees = await locationService.GetAllEmployeesByBusinessLocationId(id);
        return StatusCode(StatusCodes.Status200OK, employees);
    }

    [HttpPost("{id:int}/employees/add")]
    public async Task<IActionResult> AddLocationToEmployee(int id, [FromBody] int employeeId)
    {
        await locationService.AddEmployeeToBusinessLocation(id, employeeId);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpPost("{id:int}/employees/{employeeId:int}/delete")]
    public async Task<IActionResult> RemoveLocationFromEmployee(int id, int employeeId)
    {
        await locationService.RemoveEmployeeFromBusinessLocation(id, employeeId);
        return StatusCode(StatusCodes.Status202Accepted);
    }
}