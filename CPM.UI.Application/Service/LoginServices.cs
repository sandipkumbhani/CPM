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
    public class LoginServices : ILoginServices
    {
        private readonly ILoginAdaptor _loginAdaptor;
        public LoginServices(ILoginAdaptor loginAdaptor)
        {
            _loginAdaptor = loginAdaptor;
        }

        public Task<string> AddUser(LoginDto model)
        {
            return  _loginAdaptor.AddUserAsync(model);
        }

        public async Task<LoginDto> GetByEmail(string email)
        {
         return await _loginAdaptor.GetByEmailAsync(email);
        }

        public async Task<string> Login(LoginViewModel model)
        {
            return await _loginAdaptor.PostApiDataAsync(model);
        }

        public async Task<string> UpdateUser(int id, LoginDto model)
        {
            return await _loginAdaptor.UpdateUserAsync(id, model);
        }
    }
}
