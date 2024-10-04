using System.Net.Http.Json;

namespace Client_Side
{
    public class Program
    {
        public static readonly HttpClient httpClient = new HttpClient();

        public static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7026/api/Employees/");

            await GetAllEmployees();

            await GetEmployeeByID(1);
            await GetEmployeeByID(-1);
            await GetEmployeeByID(100);
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
    }
}