using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagmentVen.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentVen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeRepository;

        public EmployeeController(IEmployee employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public ActionResult Get()
        {
       
           
            try
            {
                return Ok(_employeeRepository.GetEmps());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error From DB");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var emp = await _employeeRepository.GetEmployee(id);
                if (emp==null)
                {
                    return NotFound();

                }
                return emp;
            }
            catch (Exception)
            {

                return Content("Errrrrrrrrr");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee emp)
        {
            try
            {
                if (emp == null)
                {
                    return BadRequest();

                }
                var employee = _employeeRepository.GetEmployeeByEmail(emp.Email);
                if (employee!=null)
                {
                    ModelState.AddModelError("Email", "This Email Already Exists");
                    return BadRequest(ModelState);

                }
                var Created = await _employeeRepository.AddEmployee(emp);

                return CreatedAtAction(nameof(GetEmployee), new { id = Created.EmployeeId },Created);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error From DB");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id,Employee employee)
        {
            try
            {
                if (id !=employee.EmployeeId)
                {
                    return BadRequest("Employee Id Mismatch");

                }
                var EmployeeToUpdate = await _employeeRepository.GetEmployee(id);
                if (EmployeeToUpdate ==null)
                {
                    return NotFound($"Not Found Employee with id={id}"); 

                }
                return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {

                throw;
            }

            
        }public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var EmployeeToDelete = await _employeeRepository.GetEmployee(id);
                if (EmployeeToDelete == null)
                {
                    return NotFound($"Not Found Employee with id={id}");

                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name,Gender? gender)
        {
            try
            {
                var result = await _employeeRepository.SearchEmployee(name, gender);

                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
