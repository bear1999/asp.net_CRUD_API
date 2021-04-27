﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
//Token
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestAPICrud.EmployeeData;
using RestAPICrud.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> checkLogin(Employee employee)
        {
            var checkLogin = await _employeeData.checkLogin(employee.Username);
            if (checkLogin != null)
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
                        new Claim(ClaimTypes.Name, employee.Username),
                        new Claim("UserId", checkLogin.Id.ToString())
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
                //'()?' if NULL then return NULL else '?' not exist and value NULL then return Error
                var value = claims.Where(x => x.Type == "UserId").FirstOrDefault()?.Value;
                return Ok(new { message = value });
            }
            return NotFound();
        }
    }
}