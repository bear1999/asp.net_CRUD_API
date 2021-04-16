using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPICrud.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {
        private List<Employee> employees = new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.NewGuid(),
                Username = "Haha 1"
            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Username = "Haha 2"
            }
        };
        public Employee AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(Guid id)
        {
            return employees.SingleOrDefault(x => x.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }
    }
}
