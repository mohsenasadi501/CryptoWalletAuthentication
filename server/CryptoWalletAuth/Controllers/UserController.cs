using Microsoft.AspNetCore.Mvc;

namespace CryptoWalletAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public Int64 GetNonce()
        {
            _dataContext.Users.ToList();
            return 1;
        }
    }
}
