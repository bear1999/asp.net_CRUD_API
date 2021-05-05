using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestAPICrud.Helpers;
using RestAPICrud.Requests;

namespace RestAPICrud.Controllers
{
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly IEmailService sendMail;

        public SendMailController(IEmailService _sendMail)
        {
            sendMail = _sendMail;
        }

        [HttpPost("api/[controller]")]
        public IActionResult SendMail(SendMailRequest emailR)
        {
            try
            {
                sendMail.SendMail(emailR.from, emailR.to, emailR.subject, emailR.html);
                return Ok(new { message = "Email send!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}
