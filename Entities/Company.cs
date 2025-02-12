using System;

namespace EmployeePortal.Entities;

public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
}
