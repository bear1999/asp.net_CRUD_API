using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPICrud.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Username can only be 50 characters long")]
        public String Username { get; set; }
        [MaxLength(100, ErrorMessage = "Max Length 100")]
        public String ProfileImage { get; set; }
    }
}
