using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWalletAuth
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? PublicAddress { get; set; }
        public long Nonce { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
