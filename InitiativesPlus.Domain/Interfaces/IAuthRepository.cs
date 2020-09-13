using InitiativesPlus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitiativesPlus.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string pasword);
        Task<bool> UserExists(string username);
    }
}
