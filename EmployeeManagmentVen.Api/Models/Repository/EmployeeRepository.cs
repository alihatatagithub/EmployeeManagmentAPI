using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentVen.Api.Models.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;

        }
      public async  Task<Employee> GetEmployeeByEmail(string email)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);

        }

        public async Task<Employee> DeleteEmployee(int employeeid)
        {
            var result =await appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeid);
            if (result != null)
            {
                appDbContext.Employees.Remove(result);
               await appDbContext.SaveChangesAsync();
            }
            return result;

        }

        public async Task<Employee> GetEmployee(int employeeid)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeid);
        }

        public IEnumerable<Employee> GetEmps()
        {
            return appDbContext.Employees.ToList();
        }

        public async Task<Employee> UpdateEmployee(Employee newemployee)
        {
            var result = await appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == newemployee.EmployeeId);
            if (result != null)
            {
                result.FirstName = newemployee.FirstName;
                result.LastName = newemployee.LastName;
                result.Email = newemployee.Email;
                result.DateOfBirth = newemployee.DateOfBirth;
                result.Gender = newemployee.Gender;
                result.DepartmentId = newemployee.DepartmentId;
                result.PhotoPath = newemployee.PhotoPath;
                await appDbContext.SaveChangesAsync();

            }
            return result;


        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string name,Gender? gender)
        {
            
            IQueryable<Employee> query = appDbContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender !=null)
            {
                query = query.Where(e => e.Gender == gender);

            }
            return await query.ToListAsync();
        }
    }

}
