using System;

namespace EmployeePortal.Entities;

public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public DateTime? CreatedOn { get; set; } = DateTime.Now;
    public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    public bool isActive { get; set; } = true;
    public List<Employee>? Employees { get; set; }
}
