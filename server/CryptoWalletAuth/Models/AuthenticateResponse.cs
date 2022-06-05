namespace CryptoWalletAuth
{
    public record AuthenticateResponse(string Message, string AccessToken, string RefreshToken);
}
