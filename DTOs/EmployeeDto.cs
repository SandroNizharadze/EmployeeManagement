using System;

namespace EmployeePortal.DTO;

public class CreateEmployeeDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Salary { get; set; }
}

public class UpdateEmployeeDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Salary { get; set; }
}
