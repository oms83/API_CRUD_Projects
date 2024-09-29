using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server_Side.Controllers.Validation;
using Server_Side.Data_Simulation;
using Server_Side.Model;

namespace Server_Side.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesAPIController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllEmployees", Name = "GetAllEmployees")]
        public ActionResult<List<clsEmployee>> GetAllEmployees()
        {
            
            List<clsEmployee> employees = EmployeesDataSimulation.EmployeesList;

            if (employees.Count == 0)
            {
                return NotFound("No employees to list");
            }

            return Ok(employees);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetTerminatedEmployees", Name = "GetTerminatedEmployees")]
        public ActionResult<List<clsEmployee>> GetTerminatedEmployees()
        {
            List<clsEmployee> terminatedEmps = EmployeesDataSimulation.EmployeesList.Where(e => e.TerminationDate is not null).ToList();

            if (terminatedEmps.Count == 0)
            {
                return NotFound("No employees are laid off.");
            }

            return Ok(terminatedEmps);

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetEmployeesWithHigherSalary/{salary}", Name = "GetEmployeesWithHigherSalary")]
        public ActionResult<List<clsEmployee>> GetEmployeesWithHigherSalary(decimal salary)
        {
            if (salary <= 0)
            {
                return BadRequest("Not Accepted Salary");
            }

            
            List<clsEmployee> employees = EmployeesDataSimulation.EmployeesList.Where(e => e.Salary > salary).ToList();

            if (!employees.Any(e => e.Salary > salary))
            {
                return BadRequest($"The number entered is actually greater than all the employees' salaries.");
            }

            if (employees.Count == 0)
            {
                return NotFound($"There are no employees with salaries more than {salary}");
            }

            return Ok(employees);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetEmployeeByID")]
        public ActionResult<clsEmployee> GetEmployeeByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not Accepted ID");
            }

            clsEmployee? employee = EmployeesDataSimulation.EmployeesList.Find(e => e.Id == id);

            if (employee == null)
            {
                return NotFound($"No Employee With ID {{{id}}}");
            }

            return Ok(employee);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddNewEmployee", Name = "AddNewEmployee")]
        public ActionResult<clsEmployee> AddNewEmployee(clsEmployee employee)
        {
            int empID = EmployeesDataSimulation.EmployeesList.Count + 1;

            employee.Id = empID;
            
            if (!(new clsEmployeeValidator().Validate(employee).IsValid))
            {
                return BadRequest("Invalid employee data");
            }

            EmployeesDataSimulation.EmployeesList.Add(employee);

            return CreatedAtRoute("", empID, employee);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}", Name = "DeleteStudent")]
        public ActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not Accepted ID");
            }

            clsEmployee? employee = EmployeesDataSimulation.EmployeesList.Find(e => e.Id == id);

            if (employee == null)
            {
                return NotFound($"No Employee With ID {{{id}}}");
            }

            EmployeesDataSimulation.EmployeesList.Remove(employee);

            return Ok($"Student with id {{{id}}} has been deleted");
        }
    }

}
