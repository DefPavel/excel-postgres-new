using System.Net;
using System.Text;
using System.Text.Json;
using excel_postgres_new.Models;
using excel_postgres_new.Models.Students;

namespace excel_postgres_new.Utils;

public static class HttpClientUtil
{
    private const string Host = "http://jmu.api.lgpu.org/";
    
    public static async Task DownloadFileTaskAsync(this HttpClient client, string uri, string fileName)
    {
        try
        {
            // Замена пробелов если такие есть
            var replace = uri.Replace(" ", "%20");

            await using var s = await client.GetStreamAsync(new Uri(Host + replace));
            await using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
            await s.CopyToAsync(fs);
        }
        // Заглушка
        catch (Exception)
        {
            Console.WriteLine(Host + uri);
            
            //throw;
        }
    }
    
      public static async Task<User> GetUserToken(this HttpClient httpClient)
        {
            var user = new User()
            {
                UserName = "1978",
                Password = "005e7dbc17761eeaf487d484151c3536:c4ec37b4060cb9fbf50f4c4627b1c58c",
                IdModules = ModulesProject.Personnel
            };
            using var content = new StringContent(ConvertToJsonString(user), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Host + "api/auth", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(jsonString)
                   ?? throw new NullReferenceException();
        }
       // Это чисто Славика 
        public static async Task<ReportData> GetStudents(this HttpClient httpClient , string token)
        {
            if (!httpClient.DefaultRequestHeaders.Contains("auth-token"))
            {
                httpClient.DefaultRequestHeaders.Add("auth-token", token);
            }
            var response = await httpClient.GetAsync(Host + "api/education/students/skud/all");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ReportData>(jsonString)
                   ?? throw new NullReferenceException();
        }
        
        // Получить Сотрудников Штат
        public static async Task<IEnumerable<Employee>> GetEmployeesIsMain(this HttpClient httpClient , string token)
        {
            if (!httpClient.DefaultRequestHeaders.Contains("auth-token"))
            {
                httpClient.DefaultRequestHeaders.Add("auth-token", token);
            }
            var response = await httpClient.GetAsync(Host + "api/pers/person/get/is_main");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Employee>>(jsonString)
                   ?? throw new NullReferenceException();
        }
        // Получить Сотрудников Внеш. Совместители
        public static async Task<Employee> GetEmployeesIsPluralist(this HttpClient httpClient , string token)
        {
            if (!httpClient.DefaultRequestHeaders.Contains("auth-token"))
            {
                httpClient.DefaultRequestHeaders.Add("auth-token", token);
            }
            var response = await httpClient.GetAsync(Host + "api/pers/person/get/out_pluralist");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Employee>(jsonString)
                   ?? throw new NullReferenceException();
        }

        private static string ConvertToJsonString(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
}