using EmployeePortal.Entities;
using EmployeePortal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeePortal.DTOs;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TaskManagementController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> AddEpic(string epicNo, string epicDescription, Guid companiId) {
            _context.Epics.Add(new Epic {
                EpicNO = epicNo,
                Description = epicDescription,
                CompanyId = companiId
            });
            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpGet]
        [Route("{id}:guid")]
        public async Task<IActionResult> GetEmloyeeReviewById(Guid id)
        {
            try {
                var employee = await _context.Employees
                .Include(e => e.EmployeeEpics!)
                .ThenInclude(e => e.Epic!)
                .FirstOrDefaultAsync(x => x.Id == id.ToString());

                if (employee == null || employee.Company == null) {
                    return NotFound("Employee or Company not found");
                }

                var returnObject = new EmployeeReviews {
                    CompanyName = employee.Company.Name,
                    EmployeeName = employee.Nickname,
                    PhoneNumber = employee.PhoneNumber,
                    EpicNames = employee.EmployeeEpics?.Select(x => x.Epic?.EpicNO ?? string.Empty).ToList()
                };

                return Ok(returnObject);
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }   
        }
    }
}
