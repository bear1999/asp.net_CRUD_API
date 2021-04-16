using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPICrud.EmployeeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPICrud.Controller
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeData _employeeData;

        public EmployeeController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployees(Guid Id)
        {
            var employees = _employeeData.GetEmployee(Id);
            if (employees != null)
            {
                return Ok(employees);
            }
            return NotFound($"Not found Employee with Id: {Id}");
        }

    }
}
