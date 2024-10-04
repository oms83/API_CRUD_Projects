using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Side
{
    public class clsEmployee
    {
        public clsEmployee(int id, string? firstName, string? lastName, short age, decimal salary, DateTime? hireDate, DateTime? terminationDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
            HireDate = hireDate;
            TerminationDate = terminationDate;
        }
        public clsEmployee()
        {

        }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public short Age { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; } = DateTime.Now;
        public DateTime? TerminationDate { get; set; } = null;

        public override string ToString()
        {
            return $"ID:{Id}, Full Name:{FirstName} {LastName}, Age:{Age}, Salary:{Salary:C}, Hire-Data:{HireDate?.ToString("yyyy-MM-dd") ?? "N/A"}, Termination-Date:{TerminationDate?.ToString("yyyy-MM-dd") ?? "N/A"}";
        }
    }
}
