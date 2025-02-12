using System;
using EmployeePortal.DTO;

namespace EmployeePortal.Services;

public interface IEmployeesService
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(Guid id);
    Task AddEmployeeAsync(CreateEmployeeDto createEmployeeDto);
    Task<Employee> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto);
    Task DeleteEmployeeAsync(Guid id);
}
