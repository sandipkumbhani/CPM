using CPM.UI.Application.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Service
{
    public class PasswordHasherService : IPasswordHasher
    {
        private readonly IPasswordHasher<object> _passwordHasher;
        public PasswordHasherService()
        {
            _passwordHasher = new PasswordHasher<object>();
        }
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashPassword, string providePassword)
        {
            var providePasswordHasher = HashPassword(providePassword);
            var result = _passwordHasher.VerifyHashedPassword(null, hashPassword, providePassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
