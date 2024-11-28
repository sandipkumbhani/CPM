using CPM.UI.Domain.Interfaces;
using CPM.UI.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Inftrastucture.Provider
{
    public class SkillAdaptor : ISkillAdaptor
    {
        public async Task<IEnumerable<SkillMasterDto>> GetAllSkillAsync()
        {
            var _httpClient = new HttpClient();
            var response = await _httpClient.GetAsync("https://localhost:5001/api/SkillMaster/GetAllSkill");

            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var Items = JsonConvert.DeserializeObject<List<SkillMasterDto>>(Convert.ToString(responseModel.Data!));
                return Items;
            }
            return null;
        }
    }
}
