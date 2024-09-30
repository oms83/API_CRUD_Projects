

using Client_Side;
using System.Net.Http.Json;

public static class Utatlity
{
    public static void Print(this List<clsEmployee> employees) => employees.ForEach(e => Console.WriteLine(e.ToString()));
}

public class Program
{
    public static readonly HttpClient httpClient = new HttpClient();
    public static async Task Main(string[] args)
    {
        httpClient.BaseAddress = new Uri("https://localhost:7299/api/Employees/");

        await GetAllEmployees();

        Console.ReadKey();
    }
    static async Task GetAllEmployees()
    {
        try
        {
            List<clsEmployee>? employees = await httpClient.GetFromJsonAsync<List<clsEmployee>>("GetAllEmployees");

            if (employees == null)
            {
                Console.WriteLine("Bad Request: No Data");
                //return;
            }

            if (employees?.Count == 0)
            {
                Console.WriteLine("Not Found: No employees to list");
            }

            employees?.Print();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

}