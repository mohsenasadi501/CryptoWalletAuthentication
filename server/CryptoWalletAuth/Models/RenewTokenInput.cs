namespace CryptoWalletAuth.Models
{
    public class RenewTokenInput
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
