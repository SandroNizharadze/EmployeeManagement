using EmployeePortal.DTO;
using EmployeePortal.Entities;
using EmployeePortal.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto createEmployeeDto)
        {
            await _employeeService.AddEmployeeAsync(createEmployeeDto);
            return Ok();
        }

        [HttpPut]
        [Route("update/{id:guid}")] 
        [Authorize]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
            return Ok(employee);
        }

        [HttpDelete]
        [Route("delete/{id:guid}")] 
        [Authorize]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }

        [HttpDelete]
        [Route("turnicate/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> TurnicateEmployee(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
