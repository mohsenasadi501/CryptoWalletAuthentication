using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWalletAuth.Models.DataModel
{
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
