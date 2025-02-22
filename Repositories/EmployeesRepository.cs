using System;
using EmployeePortal.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(Guid id) => 
        await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new Exception($"Employee with id {id} not found.");

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        var employee = await GetEmployeeByIdAsync(id);
        _context.Employees.Remove(employee);
        
        await _context.SaveChangesAsync();
    }
}

