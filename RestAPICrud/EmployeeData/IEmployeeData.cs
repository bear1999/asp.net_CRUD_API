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
        Employee EditEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);

    }
}
