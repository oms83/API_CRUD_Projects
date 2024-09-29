using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return NotFound("Not employees to list");
            }

            return Ok(employees);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetTerminatedEmployees", Name = "GetTerminatedEmployees")]
        public ActionResult<List<clsEmployee>> GetTerminatedEmployees()
        {
            List<clsEmployee> terminatedEmps = EmployeesDataSimulation.EmployeesList.Where(e => e.TerminationDate is null).ToList();

            if (terminatedEmps.Count == 0)
            {
                return NotFound("No employees are laid off.");
            }

            return Ok(terminatedEmps);

        }

    }

}
