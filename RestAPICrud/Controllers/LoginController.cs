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
using RestAPICrud.Servcies.Employee;
using Microsoft.AspNetCore.Authorization;
using RestAPICrud.ViewModels;
using Microsoft.Extensions.Options;
using RestAPICrud.Helpers;

namespace RestAPICrud.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly AppSettings _appSettings;

        //Contructor
        public LoginController(IEmployeeService employeeData, IOptions<AppSettings> appSettings)
        {
            _employeeService = employeeData;
            _appSettings = appSettings.Value;
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> CheckLogin(LoginViewModel emp)
        {
            var checkLogin = await _employeeService.CheckLogin(emp.Username);
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
                        new Claim("Id", checkLogin.Id.ToString()),
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

        [Authorize(Policy = "isDelete")]
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

        //[HttpGet("api/[controller]/{id}")]
        //public async Task<IActionResult> Test(Guid Id)
        //{
        //    var employees = await _employeeService.CheckIsDelete(Id);
        //    if (employees != null)
        //    {
        //        return Ok(employees.IsDelete);
        //    }
        //    return NotFound(new { message = $"Not found Employee with Id: {Id}" });
        //}
    }
}
