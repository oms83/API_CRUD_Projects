﻿using Data_Access;
using Data_Business;
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
        [HttpGet("GetAllEmployees", Name = "GetAllEmployees")]
        public ActionResult<IEnumerable<clsEmployeeDTO>> GetAllEmployees()
        {
            IEnumerable<clsEmployeeDTO> employees = new List<clsEmployeeDTO>();

            employees = Data_Business.Employee.GetAllEmployees();

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


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetActiveEmployees", Name = "GetActiveEmployees")]
        public ActionResult<IEnumerable<clsEmployeeDTO>> GetActiveEmployees()
        {
            IEnumerable<clsEmployeeDTO> employees = new List<clsEmployeeDTO>();

            employees = Data_Business.Employee.GetActiveEmployees();

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


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = "GetEmployeeByID")]
        public ActionResult<clsEmployeeDTO> GetEmployeeByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not Accepted Employee ID");
            }

            Employee emp = Data_Business.Employee.GetEmployeeByID(id);

            if (emp is null)
            {
                return NotFound($"No Student With ID {{{id}}}");
            }

            clsEmployeeDTO empDTO = emp.EmployeeDTO;

            return Ok(empDTO);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddNewEmployee", Name = "AddNewEmployee")]
        public ActionResult<clsEmployeeDTO> AddNewEmployee(clsEmployeeDTO employeeDTO)
        {
            if (!EmployeeValidator.Validator(false).Validate(employeeDTO).IsValid)
            {
                return BadRequest("Invalid Employee Data");
            }

            Employee employee = new Employee(employeeDTO, Employee.enMode.AddNew);

            if (employee.Save())
            {
                employeeDTO = employee.EmployeeDTO;

                return CreatedAtRoute("", employee.Id, employeeDTO);
            }

            return BadRequest("Employee was not inserted successfully");
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}", Name = "UpdateEmployee")]
        public ActionResult<clsEmployeeDTO> UpdateEmployee(int id, clsEmployeeDTO employeeDTO)
        {
            if (id <= 0)
            {
                return BadRequest("Not Accepted Employee ID");
            }

            if (!EmployeeValidator.Validator(false).Validate(employeeDTO).IsValid)
            {
                return BadRequest("Invalid Employee Data");
            }

            Employee employee = Employee.GetEmployeeByID(id);

            if (employee is null)
            {
                return NotFound($"No Employee With ID{{{id}}}");
            }
            
            employee.FirstName = employeeDTO.FirstName;
            employee.LastName = employeeDTO.LastName;
            employee.Salary = employeeDTO.Salary;
            employee.TerminationDate = employeeDTO.TerminationDate;
            employee.HireDate = employeeDTO.HireDate;
            employee.Age = employeeDTO.Age;
            
            if (employee.Save())
            {
                employeeDTO = employee.EmployeeDTO;

                return Ok(employeeDTO);
            }

            return BadRequest("Employee was not updated successfully");
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public ActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Not Accepted Employee ID {{{id}}}");
            }

            Employee employee = Employee.GetEmployeeByID(id);

            if (employee is null)
            {
                return NotFound($"No Employee With ID{{{id}}}");
            }

            if (Employee.DeleteEmployee(id))
            {
                return Ok($"Employee With ID{{{id}}} Has Been Deleted Successfully");
            }

            return BadRequest($"Failed to delete Employee with ID{{{id}}}.");
        }
    }
}
