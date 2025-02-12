using EmployeePortal.DTO;
using EmployeePortal.DTOs;
using EmployeePortal.Entities;
using EmployeePortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService _companyService;

        public CompaniesController(ICompaniesService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpGet]
        [Route("Details/{id:guid}")]
        public async Task<IActionResult> GetCompanyDetailsById(Guid id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCompany([FromBody] CreateCompanyDto createCompanyDto)
        {
            Console.WriteLine($"Received: Name = {createCompanyDto.Name}, Address = {createCompanyDto.Address}");
            await _companyService.AddCompanyAsync(createCompanyDto);
            return Ok();
        }

        [HttpPut]
        [Route("update/{id:guid}")] // api/Companies/UpdateCompany?/id=
        public async Task<IActionResult> UpdateCompany(Guid id, UpdateCompanyDto updateCompanyDto)
        {
            var company = await _companyService.UpdateCompanyAsync(id, updateCompanyDto);
            return Ok(company);
        }

        [HttpDelete]
        [Route("delete/{id:guid}")] // api/Companies/DeleteCompany?/id=
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return Ok();
        }
    }
}
