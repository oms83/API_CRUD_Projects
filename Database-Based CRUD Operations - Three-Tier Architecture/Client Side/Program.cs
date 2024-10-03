
using System.Net.Http.Json;
using System.Threading.Channels;

namespace Client_Side
{
    public class Program
    {
        public static readonly HttpClient httpClient = new HttpClient();
        public static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7074/api/Employees/");

            //await GetAllEmployees();

            await GetActiveEmployees();
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

    }
}