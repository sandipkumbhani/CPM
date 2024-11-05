using CPM.UI.Application.Interface;
using CPM.UI.Domain.Interfaces;
using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Service
{
    public class SkillServices : ISkillServices
    {
        private readonly ISkillAdaptor _skillAdaptor;
        public Task<IEnumerable<SkillMasterDto>> GetAllSkill()
        {
            return _skillAdaptor.GetAllSkillAsync();
        }
    }
}
