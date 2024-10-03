
using System.Net.Http.Json;
using System.Threading.Channels;

namespace Client_Side
{
    public class Program
    {
        public static readonly HttpClient httpClient = new HttpClient();

        private static clsEmployee employee = new clsEmployee()
        {
            FirstName = "Ali",
            LastName = "Sultan",
            Age = 22,
            Salary = 24000m,
            HireDate = DateTime.Now,
            TerminationDate = null,
        };
        private static clsEmployee invalidEmployeeData = new clsEmployee()
        {
            FirstName = "Al",
            LastName = "s",
            Age = 80,
            Salary = -24000m,
            HireDate = DateTime.Now,
            TerminationDate = null,
        };

        public static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7074/api/Employees/");

            //await GetAllEmployees();

            //await GetActiveEmployees();

            //await GetEmployeeByID(-1);
            //await GetEmployeeByID(1);
            //await GetEmployeeByID(333);

            //await AddNewEmployee(employee);
            //await AddNewEmployee(invalidEmployeeData);

            //await UpdateEmployee(20, employee);
            //await UpdateEmployee(200, employee);
            //await UpdateEmployee(-20, employee);

            await DeleteEmployee(20);
            await DeleteEmployee(200);
            await DeleteEmployee(-20);
        }

        static async Task GetAllEmployees()
        {
            try
            {
                var employees = await httpClient.GetFromJsonAsync<List<clsEmployee>>("GetAllEmployees");

                if (employees == null)
                {
                    Console.WriteLine("BadRequest: Data was not fetched successfully.");
                    return;
                }

                if (employees.Count == 0)
                {
                    Console.WriteLine("NotFound: No Data To List");
                }

                employees.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task GetActiveEmployees()
        {
            try
            {
                var employees = await httpClient.GetFromJsonAsync<List<clsEmployee>>("GetActiveEmployees");

                if (employees == null)
                {
                    Console.WriteLine("BadRequest: Data was not fetched successfully.");
                    return;
                }

                if (employees.Count == 0)
                {
                    Console.WriteLine("NotFound: No Data To List");
                }

                employees.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task GetEmployeeByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    Console.WriteLine("BadRequest: Not Accepted Employee ID");
                    return;
                }

                var response = await httpClient.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    clsEmployee? employee = await response.Content.ReadFromJsonAsync<clsEmployee>();

                    if (employee is not null)
                    {
                        Console.WriteLine(employee);
                    }
                    else
                    {
                        Console.WriteLine($"NotFound: No Student With ID {{{id}}}");
                        return;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"NotFound: No Student With ID{{{id}}}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not Accepted ID{{{id}}}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task AddNewEmployee(clsEmployee employee)
        {
            try
            {
                if (!clsEmployeeValidator.Validator(false).Validate(employee).IsValid)
                {
                    Console.WriteLine("BadRequest: Invalid Employee Data");
                    return; 
                }

                var response = await httpClient.PostAsJsonAsync<clsEmployee>("AddNewEmployee", employee);

                if (response.IsSuccessStatusCode)
                {
                    clsEmployee? insertedEmployee = await response.Content.ReadFromJsonAsync<clsEmployee>();

                    if (insertedEmployee is not null)
                    {
                        Console.WriteLine(insertedEmployee);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("BadRequest: Invalid Employee Data");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task UpdateEmployee(int id, clsEmployee employee)
        {
            try
            {
                if (id <= 0)
                {
                    Console.WriteLine("BadRequest: Not Accepted Employee ID");
                    return;
                }
                
                if (!clsEmployeeValidator.Validator(false).Validate(employee).IsValid)
                {
                    Console.WriteLine("BadRequest: Invalid Employee Data");
                    return;
                }

                var response = await httpClient.PutAsJsonAsync<clsEmployee>($"{id}", employee);

                if (response.IsSuccessStatusCode)
                {
                    clsEmployee? updateEmployee = await response.Content.ReadFromJsonAsync<clsEmployee>();

                    if (updateEmployee is not null)
                    {
                        Console.WriteLine(updateEmployee);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("BadRequest: Invalid Employee Data");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"NotFound: No Student With ID {{{id}}}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task DeleteEmployee(int id)
        {
            try
            {
                if (id <= 0)
                {
                    Console.WriteLine("BadRequest: Not Accepted Employee ID");
                    return;
                }

                var response = await httpClient.DeleteAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Ok: Employee With ID{{{id}}} Deleted Successfully");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("BadRequest: Not Accepted Employee ID");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"NotFound: No Student With ID {{{id}}}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}