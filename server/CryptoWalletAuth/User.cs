using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWalletAuth
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? PublicAddress { get; set; }
        public Int64 Nonce { get; set; }
    }
}
