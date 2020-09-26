using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentVen.Api.Models
{
    public interface IDepartment
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int id);
    }
}
