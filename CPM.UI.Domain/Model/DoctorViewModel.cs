using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class DoctorViewModel
    {
        public DoctorViewModel()
        {
            DoctorDto = new List<DoctorViewModel>();
        }
        public List<DoctorViewModel> DoctorDto { get; set; }
    }

    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SkillId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
    }
}
