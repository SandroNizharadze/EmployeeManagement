using System;
using EmployeePortal.Entities;

namespace EmployeePortal.Repositories;

public interface ICompaniesRepository
{

    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company> GetCompanyByIdAsync(Guid id);
    Task<Company> GetCompanyDetailsByIdAsync(Guid id);
    Task AddCompanyAsync(Company company);
    Task<Company> UpdateCompanyAsync(Company company);
    Task DeleteCompanyAsync(Guid id);
}