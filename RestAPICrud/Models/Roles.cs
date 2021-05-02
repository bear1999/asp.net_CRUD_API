using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestAPICrud.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Employees = new HashSet<Employees>();
        }

        public int IdRole { get; set; }
        public string NameRole { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
