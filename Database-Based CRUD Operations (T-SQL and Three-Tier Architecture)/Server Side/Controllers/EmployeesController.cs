using Business_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server_Side.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        [HttpGet("GetAllEmplyees", Name = "GetAllEmplyees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<clsEmployeeDTO>> GetAllEmplyees()
        {
            List<clsEmployeeDTO> employees = clsEmployee.GetAllEmployees().ToList();

            if (employees is null)
            {
                return BadRequest("Data was not fetched successfully.");
            }

            if (employees.Count == 0)
            {
                return NotFound("No Data To List");
            }

            return Ok(employees);
        }
    }
}
