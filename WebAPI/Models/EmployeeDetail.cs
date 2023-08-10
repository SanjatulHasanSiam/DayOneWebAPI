using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class EmployeeDetail
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Mobile { get; set; }

    public DateTime? CreatedAt { get; set; }
}
