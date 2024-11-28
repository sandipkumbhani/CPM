using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class DoctorMasterViewModel
    {
        public DoctorMasterViewModel()
        {
            DoctorDto = new List<DoctorMasterDto>();
        }
        public List<DoctorMasterDto> DoctorDto { get; set; }
    }

    public class DoctorMasterDto
    {
        [Key]
        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? DoctorNo { get; set; }
        public DateOnly? DOB { get; set; }
        public string? DoctorEmail { get; set; }
        public int? SkillId { get; set; } = 0;
        [ForeignKey("SkillId")]
        public SkillMasterDto? SkillMaster { get; set; }
        public int? ClinicId { get; set; } 
        [ForeignKey("ClinicId")]
        public ClinicMasterDto? ClinicMaster { get; set; }
        public int? RoleId { get; set; } = 1;
        [ForeignKey("RoleId")]
        public RoleMasterDto? RoleMaster { get; set; }
        public bool IsActive { get; set; } = true;
        public int? InsBy { get; set; }
        public DateTime? InsDateTime { get; set; }
        public int? UpdBy { get; set; }
        public DateTime? UpdDateTime { get; set; }
    }
}
