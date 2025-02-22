using System;

namespace EmployeePortal.DTO;

public class GetEmployeeDto
{
    public string? Nickname { get; set; }
    public string? PhoneNumber { get; set; }
    public bool isActive { get; set; }
}
public class CreateEmployeeDto
{
    public string? Nickname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Salary { get; set; }
    public Guid? CompanyId { get; set; }
}

public class UpdateEmployeeDto
{
    public string? Nickname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Salary { get; set; }
    public Guid? CompanyId { get; set; }
}
