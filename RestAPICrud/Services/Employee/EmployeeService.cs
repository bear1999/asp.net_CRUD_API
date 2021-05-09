using Microsoft.EntityFrameworkCore;
using RestAPICrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPICrud.Servcies.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public async Task<Employees> AddEmployee(Employees employee, EmployeesInfo empInfo)
        {
            employee.Id = Guid.NewGuid();
            await _employeeContext.Employees.AddAsync(employee);

            empInfo.IdEmployee = employee.Id;
            await _employeeContext.EmployeesInfo.AddAsync(empInfo);

            await _employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Employees employee, EmployeesInfo empInfo)
        {
            _employeeContext.EmployeesInfo.Remove(empInfo);
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
        }

        public async Task<Employees> EditEmployee(Employees employee, EmployeesInfo empInfo)
        {
            var existEmployee = await _employeeContext.Employees.FindAsync(employee.Id);
            if (existEmployee != null)
            {
                existEmployee.Username = employee.Username;
                existEmployee.Password = employee.Password;
                existEmployee.IdRole = employee.IdRole;
                //Update Image Profile
                if(employee.ProfileImage != null) existEmployee.ProfileImage = employee.ProfileImage;
                //EmployeesInfo
                existEmployee.EmployeesInfo.Fullname = empInfo.Fullname;
                existEmployee.EmployeesInfo.Birthday = empInfo.Birthday;
                existEmployee.EmployeesInfo.Address = empInfo.Address;
                existEmployee.EmployeesInfo.PhoneNumber = empInfo.PhoneNumber;

                _employeeContext.Employees.Update(existEmployee);
                await _employeeContext.SaveChangesAsync();

                //Don't show password
                existEmployee.Password = null;
            }
            return existEmployee;
        }

        public async Task<Employees> GetEmployee(Guid id)
        {
            var employee = await _employeeContext.Employees
                .Include(x => x.EmployeesInfo)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                employee.Password = null;
                employee.IdRoleNavigation = null;
            }
            return employee;
        }

        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            var employee = await _employeeContext.Employees
                .Include(x => x.EmployeesInfo)
                .ToListAsync();
            if (employee != null)
            {
                employee.ForEach(x => x.IdRoleNavigation = null);
                employee.ForEach(x => x.Password = null);
            }
            return employee;
        }

        public async Task<Employees> CheckLogin(string username)
        {
            return await _employeeContext.Employees.Include(x => x.IdRoleNavigation).SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> CheckIsDelete(Guid id)
        {
            var employee = await _employeeContext.Employees
                .FirstOrDefaultAsync(x => x.Id == id);
            return employee.IsDelete;
        }
    }
}
