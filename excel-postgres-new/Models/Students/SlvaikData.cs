using System.Text.Json.Serialization;
namespace excel_postgres_new.Models.Students;
public class SlvaikData : BaseModel
{
    [JsonPropertyName("middlename")] public string MiddleName { get; set; } = "undefined";
        
    [JsonPropertyName("photo_path")] public string PathUrl { get; set; } = "undefined";
    
    [JsonPropertyName("name_department")] public string NameFaculty { get; set; } = "undefined";
        
    [JsonPropertyName("specialty_name")] public string NameSpecialty { get; set; } = "undefined";

    [JsonPropertyName("form_name")] public string Level { get; set; } = "undefined";
        
    [JsonPropertyName("group_date_start")] public string YearStart { get; set; } = "undefined";
}