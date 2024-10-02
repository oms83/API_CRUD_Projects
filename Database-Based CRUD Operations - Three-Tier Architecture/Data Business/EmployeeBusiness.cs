﻿using Data_Access;
using System;


namespace Data_Business
{
    public class Employee
    {
        public enum enMode { AddNew = 0, Update = 0 }

        private enMode _Mode = enMode.AddNew;
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public short Age { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; } = DateTime.Now;
        public DateTime? TerminationDate { get; set; } = null;

        public clsEmployeeDTO EmployeeDTO => new clsEmployeeDTO(
            this.Id,
            this.FirstName,
            this.LastName,
            this.Age,
            this.Salary,
            this.HireDate,
            this.TerminationDate
        );

        public Employee(clsEmployeeDTO dto, enMode mode)
        {
            this.Id = dto.Id;
            this.FirstName = dto.FirstName;
            this.LastName = dto.LastName;
            this.Age = dto.Age;
            this.Salary = dto.Salary;
            this.HireDate = dto.HireDate;
            this.TerminationDate = dto.TerminationDate;

            this._Mode = mode;
        }
        public static IEnumerable<clsEmployeeDTO> GetAllEmployees()
        {
            return EmployeeDataAccess.GetAllEmployees();
        }

        public static IEnumerable<clsEmployeeDTO> GetActiveEmployees()
        {
            return EmployeeDataAccess.GetActiveEmployees();
        }

        public static Employee GetEmployeeByID(int Id)
        {
            clsEmployeeDTO dto = EmployeeDataAccess.GetEmployeeByID(Id);

            if (dto is not null)
            {
                return new Employee(dto, enMode.Update);
            }
            else
            {
                return null;
            }
        }

    }
}
