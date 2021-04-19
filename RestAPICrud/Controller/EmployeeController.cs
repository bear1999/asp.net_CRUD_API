using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPICrud.EmployeeData;
using RestAPICrud.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RestAPICrud.Controller
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeData _employeeData;
        private IWebHostEnvironment _hostEnvironment;

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

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetEmployee(Guid Id)
        {
            var employees = await _employeeData.GetEmployee(Id);
            if (employees != null)
            {
                return Ok(employees);
            }
            return NotFound($"Not found Employee with Id: {Id}");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddEmployee([FromForm] Employee employee, IFormFile profileImage)
        {
            if (profileImage != null)
            {
                string extension = System.IO.Path.GetExtension(profileImage.FileName);
                if (Equals(extension, ".png") || Equals(extension, ".jpg") || Equals(extension, ".jpge"))
                {
                    var filename = DateTime.Now.ToString("ddMMyyyyHHmmss") + Guid.NewGuid() + extension;
                    var path = Path.Combine(_hostEnvironment.ContentRootPath, "Assets/ProfileImage/", filename);
                    if (System.IO.File.Exists(path))
                        return BadRequest("File path is exist");
                    else
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await profileImage.CopyToAsync(fileStream);
                        }
                        employee.ProfileImage = filename;
                        await _employeeData.AddEmployee(employee);
                        return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
                    }
                }
            }
            return BadRequest("File image is require");

        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid Id)
        {
            var employee = await _employeeData.GetEmployee(Id);

            if (employee != null)
            {
                await _employeeData.DeleteEmployee(employee);
                return Ok();
            }
            return NotFound($"Not found Employee with Id: {Id}");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditEmployee(Guid Id, Employee employee)
        {
            var existEmployee = await _employeeData.GetEmployee(Id);
            if (existEmployee != null)
            {
                employee.Id = existEmployee.Id;
                return Ok(await _employeeData.EditEmployee(employee));
            }
            return NotFound($"Not found Employee with Id: {Id}");
        }
    }
}
