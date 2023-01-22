using System.Text.Json.Serialization;

namespace excel_postgres_new.Models;

public class BaseModel
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("firstname")] public string FirstName { get; set; } = "undefined";
    [JsonPropertyName("name")] public string Name { get; set; } = "undefined";
    [JsonPropertyName("lastname")] public string LastName { get; set; } = "undefined";
}