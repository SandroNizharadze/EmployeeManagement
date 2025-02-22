using System;

namespace EmployeePortal.Entities;

public class EmployeeEpic
{
    public Guid Id { get; set; }
    public Guid EpicId { get; set; }
    public int EmployeeId { get; set; }
    public Epic? Epic { get; set; }
    public Employee? Employee { get; set; }

}

