using System.ComponentModel.DataAnnotations;

namespace RestAPICrud.Reponsitory
{
    public partial class employeeLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
