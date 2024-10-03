using Microsoft.Data.SqlClient;
using Data_Access_Layer;
using Azure.Core;

namespace Data_Access_Layer
{
    public class clsEmployeeData
    {
        public static IEnumerable<clsEmployeeDTO> GetAllEmployees()
        {
            using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
            {
                List<clsEmployeeDTO> employees = new List<clsEmployeeDTO>();

                using (SqlCommand command = new SqlCommand("SP_GetAllEmployees", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new clsEmployeeDTO()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Age = reader.GetByte(reader.GetOrdinal("Age")),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                                HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                TerminationDate = reader.IsDBNull(reader.GetOrdinal("TerminationDate"))
                                                    ? (DateTime?)null
                                                    : reader.GetDateTime(reader.GetOrdinal("TerminationDate"))

                            });
                        }
                    }
                }

                return employees;
            }
        }

        public static clsEmployeeDTO GetEmployeeByID(int id)
        {
            clsEmployeeDTO employee = new clsEmployeeDTO();

            using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetEmployeeByID", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new clsEmployeeDTO()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Age = reader.GetByte(reader.GetOrdinal("Age")),
                                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                                HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                TerminationDate = reader.IsDBNull(reader.GetOrdinal("TerminationDate"))
                                                    ? (DateTime?)null
                                                    : reader.GetDateTime(reader.GetOrdinal("TerminationDate"))
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

            }

            return employee;
        }

    }
}
