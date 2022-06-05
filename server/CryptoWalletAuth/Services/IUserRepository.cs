using CryptoWalletAuth.Models;

namespace CryptoWalletAuth.Services
{
    public interface IUserRepository
    {
        User GetUser(string publicAddress);
        long AddUser(string publicAddress);
        AuthenticateResponse Login(AuthenticateRequest loginInput);
        AuthenticateResponse RenewAccessToken(RenewTokenInput renewTokenInput);
    }
}