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
                Nickname = createEmployeeDto.Nickname,
                PhoneNumber = createEmployeeDto.PhoneNumber ?? string.Empty,
                Salary = createEmployeeDto.Salary ?? string.Empty,
                CompanyId = createEmployeeDto.CompanyId ?? Guid.Empty,
            };

            await _employeeRepository.AddEmployeeAsync(newEmployee);
        }

        public async Task<GetEmployeeDto?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null) return null;

            employee.Nickname = updateEmployeeDto.Nickname ?? employee.Nickname;
            employee.PhoneNumber = updateEmployeeDto.PhoneNumber ?? employee.PhoneNumber;
            employee.Salary = updateEmployeeDto.Salary ?? employee.Salary;
            var UpdatedOn = DateTime.Now;

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

            return new GetEmployeeDto {
                Nickname = updatedEmployee.Nickname,
                PhoneNumber = updatedEmployee.PhoneNumber,
                isActive = updatedEmployee.isActive
            };
        }

        public async Task UpdateEmployeeAsync(Guid id, bool isDeleted) {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null) return;

            employee.isActive = isDeleted;
            employee.UpdatedOn = DateTime.Now;
            await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }

        Task<Employee> IEmployeesService.UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
