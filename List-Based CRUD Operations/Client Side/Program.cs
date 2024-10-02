

using Client_Side;
using System.Net.Http.Json;
using System.Threading.Channels;

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

        //await GetAllEmployees();

        //await GetTerminatedEmployees();

        //await GetEmployeesWithHigherSalary(1000000);
        //await GetEmployeesWithHigherSalary(5000);
        //await GetEmployeesWithHigherSalary(-5000);

        //await GetEmployeeByID(-1);
        //await GetEmployeeByID(100);
        //await GetEmployeeByID(20);

        //await AddNewEmployee(new clsEmployee()
        //{
        //    FirstName = "Salih", 
        //    LastName = "Ozdemir", 
        //    Age = 30, 
        //    HireDate = DateTime.Now, 
        //    Salary = 200000m, 
        //    TerminationDate = null
        //});

        //await AddNewEmployee(new clsEmployee()
        //{
        //    FirstName = "Salih",
        //    LastName = "Ozdemir",
        //    Age = -50,
        //    HireDate = DateTime.Now,
        //    Salary = 200000m,
        //    TerminationDate = null
        //});
        
        await GetAllEmployees();
        Console.WriteLine("-------------------");
        
        await DeleteEmployee(-2);
        await DeleteEmployee(2);
        await DeleteEmployee(102);

        Console.WriteLine("-------------------");
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

    static async Task GetTerminatedEmployees()
    {
        try
        {
            List<clsEmployee>? employees = await httpClient.GetFromJsonAsync<List<clsEmployee>>("GetTerminatedEmployees");

            if (employees == null)
            {
                Console.WriteLine("Bad Request: No Data");
                //return;
            }

            if (employees?.Count == 0)
            {
                Console.WriteLine("Not Found: No employees are laid off.");
            }

            employees?.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task GetEmployeesWithHigherSalary(decimal salary)
    {
        try
        {
            if (salary <= 0)
            {
                Console.WriteLine("Bad Request: Not Accepted Salary");
                return;
            }

            var response = await httpClient.GetAsync($"GetEmployeesWithHigherSalary/{salary}");

            if (response.IsSuccessStatusCode)
            {
                var emps = await response.Content.ReadFromJsonAsync<List<clsEmployee>>();

                emps?.Print();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine("The number entered is actually greater than all the employees' salaries.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"There are no employees with salaries more than {salary}");
            }
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
                Console.WriteLine($"Not Accepted ID{{{id}}}");
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
                    Console.WriteLine("no sutdent info (null)");
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Not Found: No Student With ID{{{id}}}");
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

            if (!(new clsEmployeeValidator()).Validate(employee).IsValid)
            {
                Console.WriteLine("Bad Request: Invalid Employee Data");
                return;
            }

            var response = await httpClient.PostAsJsonAsync("AddNewEmployee", employee);

            if (response.IsSuccessStatusCode)
            {
                var newEmployee = await response.Content.ReadFromJsonAsync<clsEmployee>();

                if (newEmployee is not null)
                {
                    Console.WriteLine(newEmployee);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine("Bad Requset: Invalid Employee Data.");
                return;
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
                Console.WriteLine($"Not Accepted ID {{{id}}}");
            }

            var response = await httpClient.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Employee with id {{{id}}} has been deleted");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"No Employee With ID {{{id}}}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"Not Accepted ID {{{id}}}");
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
                Console.WriteLine($"Not Accepted ID{{{id}}}");
                return;
            }

            if (!(new clsEmployeeValidator()).Validate(employee).IsValid)
            {
                Console.WriteLine("Invalid Employee Data");
                return;
            }

            var response = await httpClient.PutAsJsonAsync<clsEmployee>($"{id}", employee);

            if (response.IsSuccessStatusCode)
            {
                var Updatedemp = await response.Content.ReadFromJsonAsync<clsEmployee>();

                Console.WriteLine(Updatedemp);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine("Invalid Employee Data");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"No Employee With ID {{{id}}}");
            }


        }
    }

}