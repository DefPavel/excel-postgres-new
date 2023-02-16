using excel_postgres_new.Models;
using excel_postgres_new.Models.Students;
using excel_postgres_new.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
namespace excel_postgres_new.Services;
public static class ExcelService
{
    private static void CreateCell(IRow currentRow, int cellIndex, string value, ICellStyle style)
    {
        var cell = currentRow.CreateCell(cellIndex);
        cell.SetCellValue(value);
        cell.CellStyle = style;
    }
    public static async Task CreateExcelStudents(string reportName, string nameSheet , SlvaikData[] arrayStudents , HttpClient client)
    {
        var workbook = new HSSFWorkbook();
        var myFont = (HSSFFont)workbook.CreateFont();
        myFont.FontHeightInPoints = 11;
        myFont.FontName = "Tahoma";
        
        // Defining a border
        var borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
        borderedCellStyle.SetFont(myFont);
        borderedCellStyle.BorderLeft = BorderStyle.Medium;
        borderedCellStyle.BorderTop = BorderStyle.Medium;
        borderedCellStyle.BorderRight = BorderStyle.Medium;
        borderedCellStyle.BorderBottom = BorderStyle.Medium;
        borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

        var sheet = workbook.CreateSheet(nameSheet);
        //Creat The Headers of the excel
        var headerRow = sheet.CreateRow(0);

        //Create The Actual Cells
        //CreateCell(headerRow, 0, "№", borderedCellStyle);
        CreateCell(headerRow, 0, "Фамилия", borderedCellStyle);
        CreateCell(headerRow, 1, "Имя", borderedCellStyle);
        CreateCell(headerRow, 2, "Отчество", borderedCellStyle);
        CreateCell(headerRow, 3, "Институт/Факультет", borderedCellStyle);
        CreateCell(headerRow, 4, "Специальность", borderedCellStyle);
        CreateCell(headerRow, 5, "Фото", borderedCellStyle);
        CreateCell(headerRow, 6, "Форма обучения", borderedCellStyle);
        CreateCell(headerRow, 7, "Год поступления", borderedCellStyle);

        // Rows Count
        var distinct = arrayStudents
            .Where(x => x.Level == "Очная")
            .DistinctBy(x => x.Id)
            .OrderBy(x => x.NameFaculty)
            .ThenBy(x => x.NameSpecialty)
            .ToArray();
        
        Console.WriteLine("Размер массива:" + arrayStudents.Length);
        
        Console.WriteLine("Размер уникального массива:" + distinct.Length);
        
        for (var i = 1; i < distinct.Length; i++)
        {
             Console.WriteLine($"Строка: {i}; Id: {distinct[i].Id}");
            var fileName = $"{i}_{distinct[i].Id}_{distinct[i].LastName}";
            var currentRow = sheet.CreateRow(i + 1);
            //CreateCell(currentRow, 0, i.ToString(), borderedCellStyle);
            CreateCell(currentRow, 0, distinct[i].LastName, borderedCellStyle);
            CreateCell(currentRow, 1, distinct[i].FirstName, borderedCellStyle);
            CreateCell(currentRow, 2, distinct[i].MiddleName, borderedCellStyle);
            CreateCell(currentRow, 3, distinct[i].NameFaculty, borderedCellStyle);
            CreateCell(currentRow, 4, $"{distinct[i].NameSpecialty}. {distinct[i].ProfileName}", borderedCellStyle);
            CreateCell(currentRow, 5, fileName + ".jpg", borderedCellStyle);
            CreateCell(currentRow, 6, distinct[i].Level == "Очная" 
                ? "Студент очной формы" 
                : "Студент заочной формы", borderedCellStyle);
            CreateCell(currentRow, 7, distinct[i].YearStart, borderedCellStyle);
           
            
            // Скачать фото
            await client.DownloadFileTaskAsync(distinct[i].PathUrl, Directory.GetCurrentDirectory() + "\\images\\" + fileName + ".jpg");
        }
        // Auto sized all the affected columns
        int lastColumNum = sheet.GetRow(0).LastCellNum;
        for (var i = 0; i <= lastColumNum; i++)
        {
            sheet.AutoSizeColumn(i);
            GC.Collect();
        }
        // Write Excel to disk 
        await using var fileData = new FileStream($"{reportName}.xls", FileMode.Create);
        workbook.Write(fileData);
    }
    
    public static async Task CreateExcelEmployee(string reportName, string nameSheet , Employee[] arrayEmployees, HttpClient client)
    {
        var distinctArray = arrayEmployees
            .Where(x => x.Card == "0" && x.FlagStudent == false)
            .OrderBy(x => x.DepartmentName)
            .ThenBy(x => x.PositionName)
            .ToArray();
        
            var workbook = new HSSFWorkbook();
            var myFont = (HSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 11;
            myFont.FontName = "Tahoma";
            
            // Defining a border
            var borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyle.SetFont(myFont);
            borderedCellStyle.BorderLeft = BorderStyle.Medium;
            borderedCellStyle.BorderTop = BorderStyle.Medium;
            borderedCellStyle.BorderRight = BorderStyle.Medium;
            borderedCellStyle.BorderBottom = BorderStyle.Medium;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

            var sheet = workbook.CreateSheet(nameSheet);
            //Creat The Headers of the excel
            var headerRow = sheet.CreateRow(0);

            //Create The Actual Cells
            // CreateCell(headerRow, 0, "№", borderedCellStyle);
            CreateCell(headerRow, 0, "Фамилия", borderedCellStyle);
            CreateCell(headerRow, 1, "Имя", borderedCellStyle);
            CreateCell(headerRow, 2, "Отчество", borderedCellStyle);
            CreateCell(headerRow, 3, "Фото", borderedCellStyle);
            CreateCell(headerRow, 4, "Отдел", borderedCellStyle);
            CreateCell(headerRow, 5, "Должность", borderedCellStyle);

            // Rows Count
            for (var i = 1; i < distinctArray.Length; i++)
            {
                Console.WriteLine($"Строка: {i}; Id: {distinctArray[i].Id}");
                
                var fileName = $"{i}_{distinctArray[i].Id}_{distinctArray[i].FirstName}";
                
                var currentRow = sheet.CreateRow(i + 1);
                
                // CreateCell(currentRow, 0, i.ToString(), borderedCellStyle);
                CreateCell(currentRow, 0, distinctArray[i].FirstName, borderedCellStyle);
                CreateCell(currentRow, 1, distinctArray[i].Name, borderedCellStyle);
                CreateCell(currentRow, 2, distinctArray[i].LastName, borderedCellStyle);
                CreateCell(currentRow, 3, fileName + ".jpg", borderedCellStyle);
                CreateCell(currentRow, 4, distinctArray[i].DepartmentName, borderedCellStyle);
                CreateCell(currentRow, 5, distinctArray[i].PositionName, borderedCellStyle);
                // Скачать фото
                await client.DownloadFileTaskAsync("storage" + distinctArray[i].PathUrl, Directory.GetCurrentDirectory() + "\\images\\" + fileName + ".jpg");
                
            }
            // Auto sized all the affected columns
            int lastColumNum = sheet.GetRow(0).LastCellNum;
            for (var i = 0; i <= lastColumNum; i++)
            {
                sheet.AutoSizeColumn(i);
                GC.Collect();
            }
            // Write Excel to disk 
            await using var fileData = new FileStream($"{reportName}.xls", FileMode.Create);
            workbook.Write(fileData);
    }
}