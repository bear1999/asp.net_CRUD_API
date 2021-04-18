using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPICrud.EmployeeData
{
    public interface IEmployeeData
    {
        Task <IEnumerable<Employee>> GetEmployees();
        Employee GetEmployee(Guid Id);
        Employee AddEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

    }
}
