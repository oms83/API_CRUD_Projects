using Data_Access_Layer;

namespace Business_Layer
{
    public class clsEmployee
    {
        public static IEnumerable<clsEmployeeDTO> GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();
        }
    }
}
