namespace Server_Side.Model
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
        public DateTime? HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

    }
}
