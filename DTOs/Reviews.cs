using System;

namespace EmployeePortal.DTOs;

public class EmployeeReviews
{
    public string? CompanyName { get; set; }
    public string? EmployeeName { get; set; }
    public string? PhoneNumber { get; set; }
    public List<string>? EpicNames { get; set; }
}
