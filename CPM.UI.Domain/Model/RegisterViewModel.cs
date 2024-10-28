using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{

    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            RegisterDto = new List<RegisterDto>();
        }
        public List<RegisterDto> RegisterDto { get; set; }
    }
    public class RegisterDto
    {
        [Key]
        public int Id { get; set; } 
        public string? ClinicName { get; set; }
        public string? DoctorName { get; set; }
        public int SkillId { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
