using System.Net.Http.Json;
using System.Text.Json;
using excel_postgres_new.Models.Students;

namespace excel_postgres_new.Services;

public static class ArrayService
{
    public static void CountsArraysNoPhoto(SlvaikData[] arrayStudents)
    {
        // Получаем уникальное количество институтов
        var distinctNameFaculty = arrayStudents.Select(x => x.NameFaculty).Distinct();

        foreach (var faculty in distinctNameFaculty)
        {
            var distinctPersonsByFaculty =
                arrayStudents
                    .Where(x => x.NameFaculty == faculty)
                    .Where(x => x.Level == "Очная")
                    .DistinctBy(x => x.Id).Count();
            
            Console.WriteLine($"{faculty} : Количество людей у которых нет фото - {distinctPersonsByFaculty};");
        }
    }

    public static void CountsArraysWithPhoto(SlvaikData[] arrayStudents)
    {
        // Получаем уникальное количество институтов
        var distinctNameFaculty = arrayStudents.Select(x => x.NameFaculty).Distinct();

        foreach (var faculty in distinctNameFaculty)
        {
            var distinctPersonsByFaculty =
                arrayStudents
                    .Where(x => x.NameFaculty == faculty)
                    .Where(x => x.Level == "Очная")
                    .DistinctBy(x => x.Id).Count();
            
            Console.WriteLine($"{faculty} : Количество людей у которых есть фото - {distinctPersonsByFaculty};");
        }
    }

    public static void CheckFiles(SlvaikData[] arrayStudents)
    {
        var distinctsArray = arrayStudents
            .Where(x => x.Level == "Очная")
            .Where(x => x.NameFaculty == "Институт физико-математического образования, информационных и обслуживающих технологий")
            .DistinctBy(x => x.Id);
        using var r = new StreamReader("D:\\Users\\vergel\\Downloads\\Telegram Desktop\\ИФМОИОТ нет фото.json");
        var items = JsonSerializer.Deserialize<List<CheckArray>>(r.ReadToEnd());

        var i = 1;
        foreach (var data in distinctsArray)
        {
            //var fullName = $"{data.LastName}_{data.FirstName}_{data.MiddleName}".Trim().ToLower();
            var filesData = items.Count(x => x.FirstName == data.LastName);

            if (filesData > 0)
            {
                Console.WriteLine($"{data.LastName} {data.FirstName} {data.MiddleName} - {i++}");
            }
        }
    }
}