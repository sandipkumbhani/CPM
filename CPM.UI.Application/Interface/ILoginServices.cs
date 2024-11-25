using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Interface
{
    public interface ILoginServices
    {

        Task<string> Login(LoginViewModel model);
        Task<string> AddUser(LoginDto model);
        Task<String> UpdateUser(int id, LoginDto model);

        Task<LoginDto> GetByEmail(string email);
    }
}
