using System.Text.Json.Serialization;

namespace excel_postgres_new.Models.Students;

public class CheckArray
{
    [JsonPropertyName("Фамилия")] public string FirstName { get; set; } = "undefined";
    
    [JsonPropertyName("Имя")] public string Name { get; set; } = "undefined";
    
    [JsonPropertyName("Отчество")] public string LastName { get; set; } = "undefined";
    
}