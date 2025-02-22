using System;
using EmployeePortal.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeePortal;

public class Employee : IdentityUser
{
    public string? Nickname { get; set; }
    public new required string PhoneNumber { get; set; }
    public required string Salary { get; set; }
    public DateTime? CreatedOn { get; set; } = DateTime.Now;
    public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    public bool isActive { get; set; } = true;
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
    public List<EmployeeEpic>? EmployeeEpics { get; set; }
}
