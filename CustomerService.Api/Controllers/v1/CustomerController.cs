using CustomerService.Application.Models.Create;
using CustomerService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers.v1;

[ApiController]
[Route("api/v1/customers")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService customerService;

    public CustomerController(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCustomers()
    {
        return Ok(await customerService.GetAllCustomers());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await customerService.GetCustomerById(id);
        return customer is null
            ? StatusCode(StatusCodes.Status404NotFound)
            : StatusCode(StatusCodes.Status200OK, customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerByNumber([FromQuery] string number)
    {
        var customer = await customerService.GetCustomerByNumber(number);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] PureCustomerModel pureCustomer)
    {
        var id = await customerService.CreateCustomer(pureCustomer);
        return StatusCode(StatusCodes.Status201Created, id);
    }

    [HttpPut("{id:int}/update")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] PureCustomerModel pureCustomer)
    {
        await customerService.UpdateCustomer(id, pureCustomer);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpDelete("{id:int}/delete")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await customerService.DeleteCustomer(id);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpGet("{id:int}/employees")]
    public async Task<IActionResult> GetCustomerEmployees(int id)
    {
        var employees = await customerService.GetAllEmployeesByCustomerId(id);
        return StatusCode(StatusCodes.Status200OK, employees);
    }

    [HttpPost("{customerId:int}/employees/add")]
    public async Task<IActionResult> AddEmployeeForCustomer(
        int customerId, [FromBody] PureEmployeeModel pureEmployee)
    {
        await customerService.AddEmployeeToCustomer(customerId, pureEmployee);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpDelete("{customerId:int}/employees/{employeeId:int}/remove")]
    public async Task<IActionResult> RemoveEmployeeFromCustomer(
        int customerId, int employeeId)
    {
        await customerService.RemoveEmployeeFromCustomer(customerId, employeeId);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpGet("{id:int}/locations")]
    public async Task<IActionResult> GetCustomerLocations(int id)
    {
        var locations = await customerService.GetAllBusinessLocationsByCustomerId(id);
        return StatusCode(StatusCodes.Status200OK, locations);
    }

    [HttpPost("{customerId:int}/locations/add")]
    public async Task<IActionResult> AddBusinessLocationForCustomer(
        int customerId, [FromBody] PureBusinessLocationModel location)
    {
        await customerService.AddBusinessLocationToCustomer(customerId, location);
        return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpDelete("{customerId:int}/locations/{locationId:int}/remove")]
    public async Task<IActionResult> RemoveBusinessLocationFromCustomer(
        int customerId, int locationId)
    {
        await customerService.RemoveBusinessLocationFromCustomer(customerId, locationId);
        return StatusCode(StatusCodes.Status202Accepted);
    }
}