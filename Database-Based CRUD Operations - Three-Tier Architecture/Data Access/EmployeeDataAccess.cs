using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

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

        public static clsEmployeeDTO GetEmployeeByID(int EmployeeID)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                string Query = @"Select * From Employees Where EmployeeID = @EmployeeID";

                clsEmployeeDTO employeeDTO = new clsEmployeeDTO();
                
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employeeDTO =  new clsEmployeeDTO()
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
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                return employeeDTO;
            }
        }

        public static int AddNewStudent(clsEmployeeDTO employeeDTO)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                int empID = -1;

                string Query = @"Insert Into Employees (FirstName, LastName, Age, Salary, HireDate, TerminationDate)
                        Values (@FirstName, @LastName, @Age, @Salary, @HireDate, @TerminationDate);
                        Select SCOPE_IDENTITY();
                        ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("FirstName", employeeDTO.FirstName);
                    command.Parameters.AddWithValue("LastName", employeeDTO.LastName);
                    command.Parameters.AddWithValue("Age", employeeDTO.Age);
                    command.Parameters.AddWithValue("Salary", employeeDTO.Salary);
                    command.Parameters.AddWithValue("HireDate", employeeDTO.HireDate);
                    if (employeeDTO.TerminationDate != null)
                    {
                        command.Parameters.AddWithValue("TerminationDate", employeeDTO.TerminationDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("TerminationDate", DBNull.Value);
                    }
                    connection.Open();

                    object result = command.ExecuteScalar();


                    if (int.TryParse(result.ToString(), out int id))
                    {
                        empID = id;
                    }
                }

                return empID;
            }
        }

        public static bool UpdateEmployee(clsEmployeeDTO employeeDTO)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                int AffectedRows = 0;
                string Query = @"Update Employees
                                Set
                                FirstName = @FirstName,
                                LastName = @LastName,
                                Age = @Age,
                                Salary = @Salary,
                                HireDate = @HireDate,
                                TerminationDate = @TerminationDate
                                Where EmployeeID = @EmployeeID;
                                ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeDTO.Id);
                    command.Parameters.AddWithValue("@FirstName", employeeDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", employeeDTO.LastName);
                    command.Parameters.AddWithValue("@Age", employeeDTO.Age);
                    command.Parameters.AddWithValue("@Salary", employeeDTO.Salary);
                    command.Parameters.AddWithValue("@HireDate", employeeDTO.HireDate);

                    if (employeeDTO.TerminationDate != null)
                    {
                        command.Parameters.AddWithValue("@TerminationDate", employeeDTO.TerminationDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@TerminationDate", DBNull.Value);
                    }

                    connection.Open();

                    AffectedRows = command.ExecuteNonQuery();
                }

                return (AffectedRows > 0);
            }
        }
    }
}
