using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentVen.Api.Models
{
   public interface IEmployee
    {
        Task<IEnumerable<Employee>> SearchEmployee(string name,Gender? gender);

        IEnumerable<Employee> GetEmps();
        Task<Employee> GetEmployee(int employeeid);
        Task<Employee> GetEmployeeByEmail(string email);

        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
       Task<Employee>  DeleteEmployee(int employeeid);
    }
}
