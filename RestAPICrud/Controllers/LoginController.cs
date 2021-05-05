using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using BC = BCrypt.Net.BCrypt;
//Token
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestAPICrud.EmployeeData;
using Microsoft.AspNetCore.Authorization;
using RestAPICrud.Reponsitory;
using Microsoft.Extensions.Options;
using RestAPICrud.Helpers;

namespace RestAPICrud.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeData _employeeData;
        private readonly AppSettings _appSettings;

        //Contructor
        public LoginController(IEmployeeData employeeData, IOptions<AppSettings> appSettings)
        {
            _employeeData = employeeData;
            _appSettings = appSettings.Value;
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> CheckLogin(LoginRequest emp)
        {
            var checkLogin = await _employeeData.CheckLogin(emp.Username);
            if (checkLogin != null && BC.Verify(emp.Password, checkLogin.Password))
            {
                string tokenKey = _appSettings.SerectKey;
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Issuer = "NgocSy",
                    Audience = "NgocSy",
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Id of JWT
                        new Claim(ClaimTypes.Name, checkLogin.Id.ToString()),
                        new Claim(ClaimTypes.Role, checkLogin.IdRoleNavigation.NameRole.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = credentials
                };
                var token = tokenHandler.CreateToken(tokenDescripter);
                return Ok(tokenHandler.WriteToken(token));
            }
            return BadRequest(new { message = "Fail login!" });
        }

        [Authorize]
        [HttpGet("api/[controller]")]
        public IActionResult GetValueToken()
        {
            //var identity = User.Identity as ClaimsIdentity;
            if (User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var value = claims.Where(x => x.Type == "UserId").FirstOrDefault()?.Value; // ? value allow null
                return Ok(new { message = value });
            }
            return NotFound();
        }
    }
}
