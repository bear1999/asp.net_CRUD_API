﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPICrud.EmployeeData;
using RestAPICrud.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace RestAPICrud.Controller
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeData _employeeData;
        private IWebHostEnvironment _hostEnvironment;

        [Required(ErrorMessage = "Image is require")]
        //[FileExtensions(Extensions = "png,jpg,jpge")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile fileImage { get; set; }

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
        public async Task<IActionResult> AddEmployee([FromForm] Employee employee)
        {
            var image = uploadImage().Result;
            if (image != null)
            {
                employee.ProfileImage = image;
                await _employeeData.AddEmployee(employee);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
            }
            return BadRequest("Image is require only .png, .jpg, .jpge");
        }

        [Route("api/[controller]")]
        public async Task<string> uploadImage()
        {
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
