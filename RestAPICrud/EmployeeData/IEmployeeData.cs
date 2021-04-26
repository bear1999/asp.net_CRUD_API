using Microsoft.Extensions.FileProviders;
using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPICrud.EmployeeData
{
    public interface IEmployeeData
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid Id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> EditEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        Task<Employee> checkLogin(string Username);
    }
}
