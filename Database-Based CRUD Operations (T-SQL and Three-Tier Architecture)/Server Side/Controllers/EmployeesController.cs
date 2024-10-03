using Business_Layer;
using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_Side.Validation;

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


        [HttpGet("{id}", Name = "GetEmployeeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsEmployeeDTO> GetEmployeeByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not Accepted Employee ID");
            }

            clsEmployee? employee = clsEmployee.GetEmployeeByID(id);

            if (employee is null)
            {
                return NotFound($"No Employee With ID: {id}");
            }

            clsEmployeeDTO dto = employee.employeeDTO;

            return Ok(dto);
        }


        [HttpPost("AddNewEmployee", Name = "AddNewEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsEmployeeDTO> AddNewEmployee(clsEmployeeDTO employeeDTO)
        {
            if (!clsEmployeeValidator.Validator(false).Validate(employeeDTO).IsValid)
            {
                return BadRequest("Invalid Employee Date");
            }

            clsEmployee employee = new clsEmployee(employeeDTO, clsEmployee.enMode.AddNew);

            if (employee.Save())
            {
                clsEmployeeDTO dto = employee.employeeDTO;

                return CreatedAtRoute("", dto.Id, dto);
            }

            return BadRequest("Employee Data Was Not Saved Successfully!");
        }


        [HttpPost("{id}", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsEmployeeDTO> UpdateEmployee(int id, clsEmployeeDTO employeeDTO)
        {
            if (id <= 0)
            {
                return BadRequest("Not Accepted ID");
            }

            if (!clsEmployeeValidator.Validator(false).Validate(employeeDTO).IsValid)
            {
                return BadRequest("Invalid Employee Date");
            }

            clsEmployee? employee = clsEmployee.GetEmployeeByID(id);

            if (employee == null)
            {
                return NotFound($"No Employee With ID:{id}");
            }

            employee.FirstName = employeeDTO.FirstName;
            employee.LastName = employeeDTO.LastName;
            employee.Age = employeeDTO.Age;
            employee.Salary = employeeDTO.Salary;
            employee.HireDate = employeeDTO.HireDate;
            employee.TerminationDate = employeeDTO.TerminationDate;

            if (employee.Save())
            {
                clsEmployeeDTO dto = employee.employeeDTO;

                return Ok(dto);
            }

            return BadRequest("Employee Data Was Not Saved Successfully!");
        }


    }
}
