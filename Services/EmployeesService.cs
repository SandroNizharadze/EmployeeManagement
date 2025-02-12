using System;
using EmployeePortal.DTO;
using EmployeePortal.Entities;
using EmployeePortal.Repositories;

namespace EmployeePortal.Services
{
    

    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeeRepository;

        public EmployeesService(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync() => 
            await _employeeRepository.GetAllEmployeesAsync();
         

        public async Task<Employee> GetEmployeeByIdAsync(Guid id) => 
            await _employeeRepository.GetEmployeeByIdAsync(id);

        public async Task AddEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var newEmployee = new Employee
            {
                Name = createEmployeeDto.Name,
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber,
                Salary = createEmployeeDto.Salary
            };

            await _employeeRepository.AddEmployeeAsync(newEmployee);
        }

        public async Task<Employee> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(updateEmployeeDto));
            }

            employee.Name = updateEmployeeDto.Name ?? employee.Name;
            employee.Email = updateEmployeeDto.Email ?? employee.Email;
            employee.PhoneNumber = updateEmployeeDto.PhoneNumber ?? employee.PhoneNumber;
            employee.Salary = updateEmployeeDto.Salary ?? employee.Salary;

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee); 

            return employee;
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
