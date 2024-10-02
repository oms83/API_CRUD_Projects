using Data_Access;
using System;


namespace Data_Business
{
    public class EmployeeBusiness
    {
        public static IEnumerable<clsEmployeeDTO> GetAllEmployees()
        {
            return EmployeeDataAccess.GetAllEmployees();
        }

        public static IEnumerable<clsEmployeeDTO> GetActiveEmployees()
        {
            return EmployeeDataAccess.GetActiveEmployees();
        }
    }
}
