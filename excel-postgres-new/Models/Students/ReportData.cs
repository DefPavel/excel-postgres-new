using System.Text.Json.Serialization;
namespace excel_postgres_new.Models.Students;
public class ReportData
{
    [JsonPropertyName("withPhoto")] public SlvaikData[]? WithPhoto { get; set; }
        
    [JsonPropertyName("withOutPhoto")] public SlvaikData[]? WithOutPhoto { get; set; }
}