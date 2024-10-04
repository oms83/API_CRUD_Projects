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

    }
}