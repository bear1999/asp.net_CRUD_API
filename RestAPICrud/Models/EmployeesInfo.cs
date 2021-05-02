using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestAPICrud.Models
{
    public partial class EmployeesInfo
    {
        public Guid IdEmployee { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual Employees IdEmployeeNavigation { get; set; }
    }
}
