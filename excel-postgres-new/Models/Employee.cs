using System.Text.Json.Serialization;

namespace excel_postgres_new.Models;

public class Employee : BaseModel
{
    [JsonPropertyName("photo")] public string PathUrl { get; set; } = "undefined";
    [JsonPropertyName("skud_card")] public string Card { get; set; } = "0";
    
    [JsonPropertyName("is_student")] public bool FlagStudent { get; set; }
    [JsonPropertyName("name_depart")] public string DepartmentName { get; set; } = "undefined";
    [JsonPropertyName("position")] public string PositionName { get; set; } = "undefined";
}