using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestAPICrud.Models
{
    public partial class Employees
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Username is require")]
        [MaxLength(50, ErrorMessage = "Username can only be 50 characters long")]
        public string Username { get; set; }
        [MaxLength(100, ErrorMessage = "Max Length 100")]
        public string ProfileImage { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int IdRole { get; set; }
        public virtual Roles IdRoleNavigation { get; set; }
    }
}
