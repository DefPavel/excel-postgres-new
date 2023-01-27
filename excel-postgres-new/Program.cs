using excel_postgres_new.Services;
using excel_postgres_new.Utils;

var startupPath = Directory.GetCurrentDirectory();

// проверка существования файла
if (!Directory.Exists(startupPath + "\\images"))
{
    Directory.CreateDirectory(startupPath + "\\images");
}

using var client = new HttpClient();
// Получить токен
var userToken =  await client.GetUserToken();

// Получить сотрудников
// var employees = await client.GetEmployeesIsMain(userToken.Token);
// var enumerable = employees as Employee[] ?? employees.ToArray();

// ExcelService.CreateExcelEmployee("test", "workers", enumerable);
//Console.WriteLine(enumerable.Length);

// Получить студентов
var students = await client.GetStudents(userToken.Token);
var enumerableWithPhoto = students.WithPhoto;
// Генерация в excel
if (enumerableWithPhoto != null)
    await ExcelService.CreateExcelStudents("Студенты у которых нет фото", "Есть фото",
        enumerableWithPhoto, client);