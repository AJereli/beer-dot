using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BeerDotApi.AuthModule.DTO;
using BeerDotApi.CommonServices;
using BeerDotApi.Database;
using BeerDotApi.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using BeerDotApi.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BeerDotApi.AuthModule
{
    public class AuthService: IAuthService
    {
        private readonly ICryptoService _cryptoService;
        private readonly BeerContext _beerContext;
        private readonly AppConfig _appConfig;
        private readonly UserRepository _userRepository;
        
        public AuthService(ICryptoService cryptoService, 
                BeerContext context, 
                AppConfig appConig,
                UserRepository userRepository)
        {
            _cryptoService = cryptoService;
            _beerContext = context;
            _appConfig = appConig;
            _userRepository = userRepository;
        }
        
        public async Task<TokenDto> Login(string email, string password)
        {

            var user = await _userRepository.Get(email);
            var hashed = user?.Password;

            if (hashed == null)
                return null;
            
            var isValidPassword = _cryptoService.ComparePasswords(hashed, password);
            
            if (isValidPassword)
            {
                return new TokenDto
                {
                    AccessToken = GenereteToken(user.Id),
                    ExpDate = DateTime.UtcNow.AddSeconds(_appConfig.JWT.ExpTime).ToString()
                };
            }

            return null;

        }

        public async Task<bool> Registration(RegistrationDto registrationDto)
        {
            var userModel = new UserEntity();
            var c = _beerContext.User.FirstOrDefault(e => e.Email == registrationDto.Email);
            if (c != null)
            {
                return false;
            }

            userModel.Email = registrationDto.Email;
            userModel.Password = _cryptoService.EncryptPass(registrationDto.Password);
            userModel.Username = registrationDto.Username;

            _beerContext.User.Add(userModel);

            return (await _beerContext.SaveChangesAsync() != 0);
        }

        private string GenereteToken(long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig.JWT.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(_appConfig.JWT.ExpTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}