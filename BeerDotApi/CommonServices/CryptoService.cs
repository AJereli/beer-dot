using BeerDotApi.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace BeerDotApi.CommonServices
{
    public class CryptoService: ICryptoService
    {
        private PasswordHasher<string> _hasher = new PasswordHasher<string>();

        public string EncryptPass(string password)
        {
            var a = _hasher.HashPassword(password, password);
            return a;
        }

        public bool ComparePasswords(string hashed, string raw)
        {
            var verify = _hasher.VerifyHashedPassword(raw, hashed, raw);
            return verify == PasswordVerificationResult.Success;
        }
    }
}