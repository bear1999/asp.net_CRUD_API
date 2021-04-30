using Microsoft.Extensions.FileProviders;
using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPICrud.EmployeeData
{
    public interface IEmployeeData
    {
        Task<IEnumerable<Employees>> GetEmployees();
        Task<Employees> GetEmployee(Guid Id);
        Task<Employees> AddEmployee(Employees employee);
        Task<Employees> EditEmployee(Employees employee);
        Task DeleteEmployee(Employees employee);
        Task<Employees> checkLogin(string username);
    }
}
