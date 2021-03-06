using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPICrud.Servcies.Employee;
using RestAPICrud.Helpers;
using RestAPICrud.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace RestAPICrud.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeData;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUploadFile _uploadFile;

        //Contructor
        public EmployeeController(IEmployeeService employeeData, IWebHostEnvironment environment, IUploadFile upFile)
        {
            _employeeData = employeeData;
            _hostEnvironment = environment;
            _uploadFile = upFile;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeData.GetEmployees());
        }

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
        public async Task<IActionResult> AddEmployee([FromForm] Employees employee, [FromForm] EmployeesInfo empInfo, IFormFile fileImage)
        {
            try
            {
                var image = _uploadFile.UploadImage(_hostEnvironment, "Assets/ProfileImage/", fileImage).Result;
                if (image != null)
                {
                    employee.ProfileImage = image;
                    employee.Password = BC.HashPassword(employee.Password);
                    await _employeeData.AddEmployee(employee, empInfo);
                    return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
                }
                return BadRequest(new { message = "fileImage require .png .jpg .jpge" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException.Message });
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
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                await _employeeData.DeleteEmployee(employee, employee.EmployeesInfo);
                return Ok(new { message = "Remove success!" });
            }
            return NotFound(new { message = $"Not found Employee with Id: {Id}" });
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditEmployee(Guid Id, 
            [FromForm] Employees employee, 
            [FromForm] EmployeesInfo empInfo,
            IFormFile fileImage)
        {
            try
            {
                var existEmployee = await _employeeData.GetEmployee(Id);
                if (existEmployee != null)
                {
                    employee.Id = existEmployee.Id;
                    employee.Password = BC.HashPassword(employee.Password);
                    //Change name Image
                    var image = _uploadFile.UploadImage(_hostEnvironment, "Assets/ProfileImage/", fileImage).Result;
                    if (image != null)
                    {
                        var path = Path.Combine(_hostEnvironment.ContentRootPath, "Assets/ProfileImage/", existEmployee.ProfileImage);
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                        employee.ProfileImage = image;
                    }
                    return Ok(await _employeeData.EditEmployee(employee, empInfo));
                }
                return NotFound(new { message = $"Not found Employee with Id: {Id}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException.Message });
            }
        }
    }
}
