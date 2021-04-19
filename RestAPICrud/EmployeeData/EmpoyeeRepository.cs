using Microsoft.EntityFrameworkCore;
using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPICrud.EmployeeData
{
    public class EmpoyeeRepository : IEmployeeData
    {
        private EmployeeContext _employeeContext;
        public EmpoyeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _employeeContext.Employees.AddAsync(employee);
            await _employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            var existEmployee = await _employeeContext.Employees.FindAsync(employee.Id);
            if (existEmployee != null)
            {
                existEmployee.Username = employee.Username;
                _employeeContext.Employees.Update(existEmployee);
                await _employeeContext.SaveChangesAsync();
            }
            return employee;
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            var employee = await _employeeContext.Employees.FindAsync(id);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeContext.Employees.ToListAsync();
        }
    }
}
