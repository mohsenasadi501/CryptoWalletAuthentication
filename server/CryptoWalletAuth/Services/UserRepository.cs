using CryptoWalletAuth.Models;
using CryptoWalletAuth.Models.DataModel;
using CryptoWalletAuth.Utility;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nethereum.Signer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CryptoWalletAuth.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly AppSettings _appSettings;

        public UserRepository(DataContext dataContext, IOptions<AppSettings> appSettings, IUserRoleRepository userRoleRepository)
        {
            _dataContext = dataContext;
            _appSettings = appSettings.Value;
            _userRoleRepository = userRoleRepository;
        }

        public User GetUser(string publicAddress) => _dataContext.Users.Where(row => row.PublicAddress == publicAddress).FirstOrDefault();
        public AuthenticateResponse Login(AuthenticateRequest loginInput)
        {
            string Message = "Invalid Credentials";
            if (string.IsNullOrEmpty(loginInput.PublicAddress)
            || string.IsNullOrEmpty(loginInput.Signature))
                return new AuthenticateResponse(Message, "", "");

            var user = _dataContext.Users.Where(x => x.PublicAddress == loginInput.PublicAddress).FirstOrDefault();
            if (user == null)
                return new AuthenticateResponse(Message, "", "");

            if (!ValidateSignature(loginInput.Signature, user.Nonce, loginInput.PublicAddress))
                return new AuthenticateResponse(Message, "", "");

            var roles = _userRoleRepository.GetRoleById(user.Id);

            Message = "Success";

            var userTokenPayload = new AuthenticateResponse(Message, GenerateToken(user, roles), GenerateRefreshToken());

            user.RefreshToken = userTokenPayload.RefreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(_appSettings.RefreshTokenExpMinute);

            UpdateRefreshToken(user);

            return userTokenPayload;
        }
        public AuthenticateResponse RenewAccessToken(RenewTokenInput renewTokenInput)
        {
            string Message = "Invalid Token";
            if (string.IsNullOrEmpty(renewTokenInput.AccessToken)
            || string.IsNullOrEmpty(renewTokenInput.RefreshToken))
                return new AuthenticateResponse(Message, "", "");

            ClaimsPrincipal principal = GetClaimsFromExpiredToken(renewTokenInput.AccessToken);

            if (principal == null)
                return new AuthenticateResponse(Message, "", "");

            string publicAddress = principal.Claims.Where(_ => _.Type == "PublicAddress").Select(_ => _.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(publicAddress))
                return new AuthenticateResponse(Message, "", "");

            var user = _dataContext.Users.Where(row => row.PublicAddress == publicAddress && row.RefreshToken == renewTokenInput.RefreshToken && row.RefreshTokenExpiration > DateTime.Now).FirstOrDefault();
            if (user == null)
                return new AuthenticateResponse(Message, "", "");

            Message = "Success";

            var userRoles = _userRoleRepository.GetRoleById(user.Id);

            var userTokenPayload = new AuthenticateResponse(Message, GenerateToken(user, userRoles), GenerateRefreshToken());

            user.RefreshToken = userTokenPayload.RefreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(_appSettings.RefreshTokenExpMinute);

            UpdateRefreshToken(user);

            return userTokenPayload;
        }
        public long AddUser(string publicAddress)
        {
            Random rnd = new();
            long number = rnd.Next(900000000, 1000000000);
            User user = new()
            {
                Nonce = number,
                PublicAddress = publicAddress,
                RefreshToken = "",
                RefreshTokenExpiration = DateTime.Now
            };
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return number;
        }

        private bool ValidateSignature(string signature, long nonce, string publicAddress)
        {
            var address = Utilities.GetAddress(signature, nonce.ToString());
            if (publicAddress == address)
                return true;
            else
                return false;
        }
        private string GenerateToken(User user, IList<UserRole> roles)
        {
            var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("PublicAddress", user.PublicAddress),
            };
            if ((roles?.Count ?? 0) > 0 && roles != null)
            {
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _appSettings.Issuer,
                audience: _appSettings.Audience,
                expires: DateTime.Now.AddMinutes(_appSettings.AccessTokenExpMinute),
                signingCredentials: credentials,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private void UpdateRefreshToken(User user)
        {
            var userRow = _dataContext.Users.Where(row => row.Id == user.Id).FirstOrDefault();
            if (userRow != null)
            {
                userRow.RefreshToken = user.RefreshToken;
                userRow.RefreshTokenExpiration = user.RefreshTokenExpiration;
            }
            _dataContext.SaveChanges();
        }
        private ClaimsPrincipal? GetClaimsFromExpiredToken(string accessToken)
        {
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidIssuer = _appSettings.Issuer,
                ValidateIssuer = true,
                ValidAudience = _appSettings.Audience,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                ValidateLifetime = false
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            var principal = jwtHandler.ValidateToken(accessToken, tokenValidationParameter, out SecurityToken securityToken);

            var jwtScurityToken = securityToken as JwtSecurityToken;
            return jwtScurityToken == null ? null : principal;
        }
    }
}
