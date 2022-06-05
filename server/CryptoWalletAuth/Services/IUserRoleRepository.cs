using CryptoWalletAuth.Models.DataModel;

namespace CryptoWalletAuth.Services
{
    public interface IUserRoleRepository
    {
        IList<UserRole> GetRoleById(int id);
    }
}
