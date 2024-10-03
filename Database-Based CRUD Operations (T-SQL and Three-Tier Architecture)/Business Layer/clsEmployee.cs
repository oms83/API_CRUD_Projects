using Data_Access_Layer;

namespace Business_Layer
{
    public class clsEmployee
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.AddNew;
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public short Age { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; } = DateTime.Now;
        public DateTime? TerminationDate { get; set; } = null;

        public clsEmployeeDTO employeeDTO
        {
            get => new clsEmployeeDTO() 
            { 
                Id = this.Id, 
                FirstName = this.FirstName, 
                LastName = this.LastName, 
                Age = this.Age, 
                Salary = this.Salary, 
                HireDate = this.HireDate, 
                TerminationDate = this.TerminationDate 
            };
        }
        public clsEmployee(clsEmployeeDTO dto, enMode mode = enMode.AddNew)
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
            return clsEmployeeData.GetAllEmployees();
        }

        public static clsEmployee? GetEmployeeByID(int id)
        {
            clsEmployeeDTO dto = clsEmployeeData.GetEmployeeByID(id);

            if (dto is not null)
            {
                return new clsEmployee(dto, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static bool DeleteEmployee(int id)
        {
            return clsEmployeeData.DeleteEmployee(id);
        }
        private bool _AddNewEmployee()
        {
            this.Id = clsEmployeeData.AddNewEmployee(this.employeeDTO);

            return this.Id != -1;
        }
        private bool _UpdateEmployee()
        {
            return clsEmployeeData.UpdateEmployee(Id, this.employeeDTO);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewEmployee())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                            return _UpdateEmployee();
            }

            return false;
        }



    }
}
