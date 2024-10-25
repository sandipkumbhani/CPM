using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class ClinicViewModel
    {
        public ClinicViewModel()
        {
            ClinicDto = new List<ClinicViewModel>();
        }
        public List<ClinicViewModel> ClinicDto { get; set; }
    }

    public class ClinicDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
