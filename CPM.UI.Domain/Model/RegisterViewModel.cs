using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class RegisterViewModel
    {
        public string? ClinicName { get; set; }
        public string? DoctorName { get; set; }
        public int SkillId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber{ get; set; }
        public string? Address { get; set; }
    }
}
