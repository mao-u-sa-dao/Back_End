using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly string _secretKey;
        public AuthController(AccountService accountService, IOptions<AppSettings> appSettings)
        {
            _accountService = accountService;
            _secretKey = appSettings.Value.SecretKey;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string userName, string passWord)
        {
            var user = await _accountService.GetUser(userName, passWord);
            if (user == null)
            {
                return BadRequest("Tài khoản hoặc mật khẩu không chính xác");
            }
            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }


        private string GenerateJwtToken(AccountUser account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("ID", account.AccountId.ToString()),
                    new Claim(ClaimTypes.Name, account.AccountName),
                    new Claim(ClaimTypes.Role, account.AccountRole.ToString()),
                    new Claim(ClaimTypes.Email, account.AccountGmail)
                    
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
