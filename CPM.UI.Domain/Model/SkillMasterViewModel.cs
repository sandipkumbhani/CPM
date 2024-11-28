using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class SkillMasterViewModel
    {
        public SkillMasterViewModel()
        {
            SkillDto = new List<SkillMasterDto>();
        } 
        public List<SkillMasterDto> SkillDto { get; set; }
    }
    public class SkillMasterDto
    {
        [Key]
        public int SkillId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
        public int? InsBy { get; set; }
        public DateTime? InsDateTime { get; set; }
        public int? UpdBy { get; set; }
        public DateTime? UpdDateTime { get; set; }
    }
}
