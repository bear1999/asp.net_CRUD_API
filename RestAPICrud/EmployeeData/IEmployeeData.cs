using RestAPICrud.Models;
using System;
using System.Collections.Generic;

namespace RestAPICrud.EmployeeData
{
    public interface IEmployeeData
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(Guid Id);
        Employee AddEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

    }
}
