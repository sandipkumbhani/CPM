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

        public SkillServices(ISkillAdaptor skillAdaptor)
        {
            _skillAdaptor = skillAdaptor;
        }

        public async Task<IEnumerable<SkillMasterDto>> GetAllSkill()
        {
            return await _skillAdaptor.GetAllSkillAsync();
        }
    }
}
