using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class LoginDto
    {
        [Key]
        public int LoginId { get; set; }
        public int? UserId { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public RoleMasterViewModel? RoleMaster { get; set; }
    }
    public class CommanResponseDto
    {
        public int? StatusCode { get; set; }

        public object Data { get; set; }

        public string? Message { get; set; }

        public string? ErrorMessage { get; set; }

    }
}
