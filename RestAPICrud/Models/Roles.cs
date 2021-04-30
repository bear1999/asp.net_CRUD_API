using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key]
        [Column("idRole")]
        public int IdRole { get; set; }
        [Required]
        [Column("name_role")]
        [StringLength(40)]
        public string NameRole { get; set; }

        [InverseProperty("IdRoleNavigation")]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
