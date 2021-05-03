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
using RestAPICrud.Models;
using Microsoft.AspNetCore.Authorization;
using RestAPICrud.Reponsitory;

namespace RestAPICrud.Controller
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IEmployeeData _employeeData;

        //Contructor
        public LoginController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> checkLogin(obj_EmployeeLogin emp)
        {
            var checkLogin = await _employeeData.checkLogin(emp.Username);
            if (checkLogin != null && BC.Verify(emp.Password, checkLogin.Password))
            {
                string tokenKey = "36a1a9edae54ba6772cc5a3c6a67d992";
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
        public IActionResult getValueToken()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var value = claims.Where(x => x.Type == "UserId").FirstOrDefault()?.Value; // ? value allow null
                return Ok(new { message = value });
            }
            return NotFound();
        }
    }
}
