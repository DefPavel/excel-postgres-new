using System.Text.Json.Serialization;

namespace excel_postgres_new.Models;

public class Employee : BaseModel
{
    [JsonPropertyName("photo")] public string PathUrl { get; set; } = "undefined";
        
    [JsonPropertyName("name_depart")] public string DepartmentName { get; set; } = "undefined";
        
    [JsonPropertyName("position")] public string PositionName { get; set; } = "undefined";
}