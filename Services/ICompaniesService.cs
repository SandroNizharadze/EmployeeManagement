using System;
using EmployeePortal.DTOs;
using EmployeePortal.Entities;

namespace EmployeePortal.Services;

public interface ICompaniesService
{
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company> GetCompanyByIdAsync(Guid id);

    Task<Company> GetCompanyDetailsByIdAsync(Guid id);
    Task AddCompanyAsync(CreateCompanyDto createCompanyDto);
    Task<Company> UpdateCompanyAsync(Guid id, UpdateCompanyDto updateCompanyDto);
    Task DeleteCompanyAsync(Guid id);
}