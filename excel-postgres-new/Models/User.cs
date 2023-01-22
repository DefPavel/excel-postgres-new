using System.Text.Json.Serialization;

namespace excel_postgres_new.Models;

public class User
{
    [JsonPropertyName("id")] public int Id { get; set; } // Id пользователя

    [JsonPropertyName("login")] public string UserName { get; set; } = string.Empty; // Логин

    [JsonPropertyName("password")] public string Password { get; set; } = string.Empty; // Пароль

    [JsonPropertyName("auth_token")] public string Token { get; set; } = string.Empty; // Токен для middleware

    [JsonPropertyName("id_module")] public ModulesProject IdModules { get; set; } // Модуль программы
}