using System.Threading.Tasks;
using BeerDotApi.AuthModule.DTO;

namespace BeerDotApi.AuthModule
{
    public interface IAuthService
    {
        Task<TokenDto> Login(string email, string password);
        Task<bool> Registration(RegistrationDto registrationDto);
    }
}