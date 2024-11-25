using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class PatientViewModel
    {
        public PatientViewModel()
        {
            PatientDtos = new List<PatientDto>();
        }
        public List<PatientDto> PatientDtos { get; set; }
    }
    public class PatientDto
    {
        [Key]
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; } 
        public string? Weight { get; set; } = null;
        public string? Height { get; set; } = null;
        public string? SmokingOrNicotine { get; set; } = null;
        public string? Physically_abled { get; set; } = null;
        public string? isDiabatice { get; set; } = null;
        public string? BP { get; set; } = null;
        public int? ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public ClinicMasterViewModel? ClinicMaster { get; set; }

    }

}
