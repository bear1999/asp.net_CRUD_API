using Microsoft.EntityFrameworkCore;
using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPICrud.EmployeeData
{
    public class EmployeeService : IEmployeeData
    {
        private EmployeeContext _employeeContext;
        public EmployeeService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public async Task<Employees> AddEmployee(Employees employee)
        {
            employee.Id = Guid.NewGuid();
            await _employeeContext.Employees.AddAsync(employee);
            await _employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Employees employee)
        {
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
        }

        public async Task<Employees> EditEmployee(Employees employee)
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

        public async Task<Employees> GetEmployee(Guid id)
        {
            return await _employeeContext.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            return await _employeeContext.Employees.ToListAsync();
        }

        public async Task<Employees> checkLogin(string username)
        {
            return await _employeeContext.Employees.Include(x => x.IdRoleNavigation).FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
