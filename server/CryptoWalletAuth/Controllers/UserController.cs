using CryptoWalletAuth.Models;
using CryptoWalletAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWalletAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userService;

        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }

        [HttpGet("nonce")]
        public long GetNonce(string publicAddress)
        {
            var item = _userService.GetUser(publicAddress);
            return item == null ? _userService.AddUser(publicAddress) : item.Nonce;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest authenticateRequest)
        {
            var response = _userService.Login(authenticateRequest);

            if (response == null)
                return BadRequest(new { message = "Authenticate Fail" });

            return Ok(response);
        }

        [HttpPost("refreshToken")]
        public IActionResult RefreshToken(RenewTokenInput renewTokenInput)
        {
            var response = _userService.RenewAccessToken(renewTokenInput);

            if (response == null)
                return BadRequest(new { message = "RefreshToken Fail" });

            return Ok(response);
        }
    }
}
