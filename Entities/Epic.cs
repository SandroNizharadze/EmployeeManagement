using System;
using static EmployeePortal.Entities.Enumerator;

namespace EmployeePortal.Entities;

public class Epic
{
    public int EpicId { get; set; }
    public string? EpicNO { get; set; }
    public string? EditDescription { get; set; }
    public Status status { get; set; } = Status.ToDo;
    public string? Description { get; set; }
    public Guid CompanyId { get; set; }
    

}
