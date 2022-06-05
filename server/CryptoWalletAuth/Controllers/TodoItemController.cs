using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWalletAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemController : Controller
    {
        [HttpGet("items")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetItems()
        {
            return Ok("Success");
        }
    }
}
