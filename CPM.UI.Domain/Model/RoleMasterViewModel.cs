using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Model
{
    public class RoleMasterViewModel
    {
        public RoleMasterViewModel()
        {
            RoleDto = new List<RoleMasterDto>();
        }
        public List<RoleMasterDto> RoleDto { get; set; }
    }
    public class RoleMasterDto
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
