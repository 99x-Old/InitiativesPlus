using InitiativesPlus.Application.ViewModels;
using System.Threading.Tasks;

namespace InitiativesPlus.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> UserExists(string username);

        Task<UserForRegisterViewModel> Register(UserForRegisterViewModel user);

        Task<UserForLoginViewModel> Login(UserForLoginViewModel user);
    }
}
