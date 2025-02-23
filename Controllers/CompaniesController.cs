using EmployeePortal.DTOs;
using EmployeePortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService _companiesService;
        private readonly ILogger<CompaniesController> _logger;
        public CompaniesController(ICompaniesService companiesService, ILogger<CompaniesController> logger)
        {
            _companiesService = companiesService;
            _logger = logger;
            _companiesService = companiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companiesService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var company = await _companiesService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> GetCompanyDetailsById(Guid id)
        {
            var company = await _companiesService.GetCompanyDetailsByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost("add")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddCompany(CreateCompanyDto createCompanyDto)
        {
            _logger.LogInformation("Attempting to add company with AdminPolicy authorization.");
            await _companiesService.AddCompanyAsync(createCompanyDto);
            return Ok();
        }

        [HttpPut("update/{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyDto updateCompanyDto)
        {
            var company = await _companiesService.UpdateCompanyAsync(id, updateCompanyDto);
            return Ok(company);
        }

        [HttpDelete("delete/{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _companiesService.DeleteCompanyAsync(id);
            return Ok();
        }
    }
}