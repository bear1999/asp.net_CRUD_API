using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestAPICrud.Models
{
    public partial class Employees
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(100)]
        public string ProfileImage { get; set; }
        [Column("idRole")]
        [Range(1, int.MaxValue, ErrorMessage = "idRole is require")]
        public int IdRole { get; set; }
        [Required]
        [StringLength(60)]
        public string Password { get; set; }

        [ForeignKey(nameof(IdRole))]
        [InverseProperty(nameof(Roles.Employees))]
        public virtual Roles IdRoleNavigation { get; set; }
    }
}
