using System.ComponentModel.DataAnnotations;

namespace CryptoWalletAuth
{
    public class AuthenticateRequest
    {
        [Required]
        public string PublicAddress { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}
