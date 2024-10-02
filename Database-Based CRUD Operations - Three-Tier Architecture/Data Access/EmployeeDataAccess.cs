using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Access
{
    public class EmployeeDataAccess
    {
        public static IEnumerable<clsEmployeeDTO> GetAllEmployees()
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                string Query = @"Select * From Employees;";

                List<clsEmployeeDTO> employeeDTOs = new List<clsEmployeeDTO>();

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeDTOs.Add(new clsEmployeeDTO()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Age = reader.GetByte(reader.GetOrdinal("Age")),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                                HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                // Handle NULL for TerminationDate
                                TerminationDate = reader.IsDBNull(reader.GetOrdinal("TerminationDate"))
                                                  ? (DateTime?)null
                                                  : reader.GetDateTime(reader.GetOrdinal("TerminationDate")),
                            });
                        }
                    }
                }

                return employeeDTOs;
            }
        }

        public static IEnumerable<clsEmployeeDTO> GetActiveEmployees()
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                string Query = @"Select * From Employees Where TerminationDate Is Null";

                List<clsEmployeeDTO> employeeDTOs = new List<clsEmployeeDTO>();

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeDTOs.Add(new clsEmployeeDTO()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Age = reader.GetByte(reader.GetOrdinal("Age")),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                                HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                // Handle NULL for TerminationDate
                                TerminationDate = reader.IsDBNull(reader.GetOrdinal("TerminationDate"))
                                                  ? (DateTime?)null
                                                  : reader.GetDateTime(reader.GetOrdinal("TerminationDate")),
                            });
                        }
                    }
                }

                return employeeDTOs;
            }
        }

    }
}
