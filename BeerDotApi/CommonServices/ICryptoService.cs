namespace BeerDotApi.CommonServices
{
    public interface ICryptoService
    {
        string EncryptPass(string password);
        bool ComparePasswords(string hashed, string raw);
    }
}