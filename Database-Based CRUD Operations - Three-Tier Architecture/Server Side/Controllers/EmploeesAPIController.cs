using Data_Access;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server_Side.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmploeesAPIController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult<IEnumerable<clsEmployeeDTO>> GetAllEmployees()
        {
            IEnumerable<clsEmployeeDTO> employees = new List<clsEmployeeDTO>();

            employees = Data_Business.EmployeeBusiness.GetAllEmployees();

            if (employees == null)
            {
                return BadRequest("Data was not fetched successfully.");
            }

            if (employees.Count() == 0)
            {
                return NotFound("No Data To List");
            }

            return Ok(employees);
        }

    }
}
