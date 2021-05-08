using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestAPICrud.Models
{
    public partial class Employees
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public int IdRole { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }

        public virtual Roles IdRoleNavigation { get; set; }
        public virtual EmployeesInfo EmployeesInfo { get; set; }
    }
}
