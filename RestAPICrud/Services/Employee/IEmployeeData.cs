using Microsoft.Extensions.FileProviders;
using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPICrud.Servcies.Employee
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employees>> GetEmployees();
        Task<Employees> GetEmployee(Guid Id);
        Task<Employees> AddEmployee(Employees employee, EmployeesInfo empInfo);
        Task<Employees> EditEmployee(Employees employee, EmployeesInfo empInfo);
        Task DeleteEmployee(Employees employee, EmployeesInfo empInfo);
        Task<Employees> CheckLogin(string username);
    }
}
