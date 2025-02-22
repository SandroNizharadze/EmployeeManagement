using System;
using EmployeePortal.DTO;
using EmployeePortal.DTOs;
using EmployeePortal.Entities;
using EmployeePortal.Repositories;

namespace EmployeePortal.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository _companyRepository;

        public CompaniesService(ICompaniesRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> GetAllCompaniesAsync() =>
            await _companyRepository.GetAllCompaniesAsync();

        public async Task<Company> GetCompanyByIdAsync(Guid id) =>
            await _companyRepository.GetCompanyByIdAsync(id);

        public async Task<Company> GetCompanyDetailsByIdAsync(Guid id) =>
            await _companyRepository.GetCompanyDetailsByIdAsync(id);

        public async Task AddCompanyAsync(CreateCompanyDto createCompanyDto)
        {
            var newCompany = new Company
            {
                Id = Guid.NewGuid(),
                Name = createCompanyDto.Name,
                Address = createCompanyDto.Address,
            };

            await _companyRepository.AddCompanyAsync(newCompany);
        }


        public async Task DeleteCompanyAsync(Guid id)
        {
            await _companyRepository.DeleteCompanyAsync(id);
        }

        public async Task<Company> UpdateCompanyAsync(Guid id, UpdateCompanyDto updateCompanyDto)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);
            if (company == null)
            {
                throw new ArgumentNullException(nameof(updateCompanyDto));
            }

            company.Name = updateCompanyDto.Name ?? company.Name;
            company.Address = updateCompanyDto.Address ?? company.Address;

            var updatedCompany = await _companyRepository.UpdateCompanyAsync(company);
            return updatedCompany;
        }
    }
}
