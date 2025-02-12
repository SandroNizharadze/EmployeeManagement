using System;

namespace EmployeePortal.DTOs;

public class CreateCompanyDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
}

public class UpdateCompanyDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    
}