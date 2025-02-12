using System;
using EmployeePortal.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repositories;

public class CompaniesRepository : ICompaniesRepository
{
    private readonly ApplicationDbContext _context;

    public CompaniesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company> GetCompanyByIdAsync(Guid id)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
        if (company == null)
        {
            throw new InvalidOperationException($"Company with id {id} not found.");
        }
        return company;
    }
    public async Task<Company> GetCompanyDetailsByIdAsync(Guid id)
    {
        var company = await _context.Companies.Include("Employees").FirstOrDefaultAsync(x => x.Id == id);

        if (company == null)
        {
            throw new InvalidOperationException($"Company with id {id} not found.");
        }
        return company;
    }

    public async Task AddCompanyAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
        var changes = await _context.SaveChangesAsync();
        
        if (changes == 0)
        {
            throw new Exception("Failed to save company to database");
        }
    }

    public async Task<Company> GetCompanyAsync(Guid id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            throw new InvalidOperationException($"Company with id {id} not found.");
        }
        return company;
    }

    public async Task<Company> UpdateCompanyAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task DeleteCompanyAsync(Guid id)
    {
        var company = await GetCompanyByIdAsync(id);
        _context.Companies.Remove(company);

        await _context.SaveChangesAsync();
    }


}
