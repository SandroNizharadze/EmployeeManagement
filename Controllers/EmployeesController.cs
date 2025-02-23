using EmployeePortal.DTO;
using EmployeePortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;

        public EmployeeController(IEmployeesService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto createEmployeeDto)
        {
            await _employeeService.AddEmployeeAsync(createEmployeeDto);
            return Ok();
        }

        [HttpPut]
        [Route("update/{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
            return Ok(employee);
        }

        [HttpDelete]
        [Route("delete/{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
