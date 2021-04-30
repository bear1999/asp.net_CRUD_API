using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPICrud.EmployeeData;
using RestAPICrud.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace RestAPICrud.Controller
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeData _employeeData;
        private IWebHostEnvironment _hostEnvironment;

        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile fileImage { get; set; }

        //Contructor
        public EmployeeController(IEmployeeData employeeData, IWebHostEnvironment environment)
        {
            _employeeData = employeeData;
            _hostEnvironment = environment;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeData.GetEmployees());
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetEmployee(Guid Id)
        {
            var employees = await _employeeData.GetEmployee(Id);
            if (employees != null)
            {
                return Ok(employees);
            }
            return NotFound(new { message = $"Not found Employee with Id: {Id}" });
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddEmployee([FromForm] Employees employee)
        {
            try
            {
                var image = uploadImage().Result;
                if (image != null)
                {
                    employee.ProfileImage = image;
                    employee.Password = BC.HashPassword(employee.Password);
                    await _employeeData.AddEmployee(employee);
                    return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
                }
                return BadRequest(new { message = "Image is require And only extension .png, .jpg, .jpge" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [NonAction]
        public async Task<string> uploadImage()
        {
            if (fileImage == null) return null;
            string extension = System.IO.Path.GetExtension(fileImage.FileName);
            if (!Equals(extension, ".png") && !Equals(extension, ".jpge") && !Equals(extension, ".jpg"))
                return null;
            var filename = DateTime.Now.ToString("ddMMyyyyHHmmss") + Guid.NewGuid() + extension;
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "Assets/ProfileImage/", filename);
            if (System.IO.File.Exists(path))
                return null;
            else
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileImage.CopyToAsync(fileStream);
                    return filename;
                }
            }
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid Id)
        {
            var employee = await _employeeData.GetEmployee(Id);
            if (employee != null)
            {
                var path = Path.Combine(_hostEnvironment.ContentRootPath, "Assets/ProfileImage/", employee.ProfileImage);
                if(System.IO.File.Exists(path)) System.IO.File.Delete(path);

                await _employeeData.DeleteEmployee(employee);
                return Ok(new { message = "Remove success!" });
            }
            return NotFound(new { message = $"Not found Employee with Id: {Id}" });
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditEmployee(Guid Id, Employees employee)
        {
            var existEmployee = await _employeeData.GetEmployee(Id);
            if (existEmployee != null)
            {
                employee.Id = existEmployee.Id;
                return Ok(await _employeeData.EditEmployee(employee));
            }
            return NotFound(new { message = $"Not found Employee with Id: {Id}" });
        }
    }
}
