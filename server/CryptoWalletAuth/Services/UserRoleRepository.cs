using CryptoWalletAuth.Models.DataModel;

namespace CryptoWalletAuth.Services
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly DataContext _dataContext;

        public UserRoleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IList<UserRole> GetRoleById(int id)
        {
            return _dataContext.UserRoles.Where(row => row.Id == id).ToList();
        }
    }
}
