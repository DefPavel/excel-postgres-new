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
        CreateCell(headerRow, 0, "№", borderedCellStyle);
        CreateCell(headerRow, 1, "Фамилия", borderedCellStyle);
        CreateCell(headerRow, 2, "Имя", borderedCellStyle);
        CreateCell(headerRow, 3, "Отчество", borderedCellStyle);
        CreateCell(headerRow, 4, "Форма обучения", borderedCellStyle);
        CreateCell(headerRow, 5, "Год поступления", borderedCellStyle);
        CreateCell(headerRow, 6, "Институт/Факультет", borderedCellStyle);
        CreateCell(headerRow, 7, "Специальность", borderedCellStyle);
        CreateCell(headerRow, 8, "Фото", borderedCellStyle);
        
        Console.WriteLine("Размер массива:" + arrayStudents.Length);
        // Rows Count
        for (var i = 1; i < arrayStudents.Length; i++)
        {
             Console.WriteLine($"Строка: {i}; Id: {arrayStudents[i].Id}");
            var fileName = $"{arrayStudents[i].Id}_{arrayStudents[i].LastName}";
            var currentRow = sheet.CreateRow(i + 1);
            CreateCell(currentRow, 0, i.ToString(), borderedCellStyle);
            CreateCell(currentRow, 1, arrayStudents[i].LastName, borderedCellStyle);
            CreateCell(currentRow, 2, arrayStudents[i].FirstName, borderedCellStyle);
            CreateCell(currentRow, 3, arrayStudents[i].MiddleName, borderedCellStyle);
            CreateCell(currentRow, 4, arrayStudents[i].Level, borderedCellStyle);
            CreateCell(currentRow, 5, arrayStudents[i].YearStart, borderedCellStyle);
            CreateCell(currentRow, 6, arrayStudents[i].NameFaculty, borderedCellStyle);
            CreateCell(currentRow, 7, arrayStudents[i].NameSpecialty, borderedCellStyle);
            CreateCell(currentRow, 8, fileName + ".jpg", borderedCellStyle);
            // Скачать фото
            await client.DownloadFileTaskAsync(arrayStudents[i].PathUrl, Directory.GetCurrentDirectory() + "\\images\\" + fileName + ".jpg");
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
    
    public static void CreateExcelEmployee(string reportName, string nameSheet , Employee[] arrayEmployees)
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
            CreateCell(headerRow, 0, "№", borderedCellStyle);
            CreateCell(headerRow, 1, "Фамилия", borderedCellStyle);
            CreateCell(headerRow, 2, "Имя", borderedCellStyle);
            CreateCell(headerRow, 3, "Отчество", borderedCellStyle);
            CreateCell(headerRow, 4, "Отдел", borderedCellStyle);
            CreateCell(headerRow, 5, "Должность", borderedCellStyle);
            
            // Rows Count
            for (var i = 1; i < arrayEmployees.Length; i++)
            {
                var currentRow = sheet.CreateRow(i + 1);
                
                CreateCell(currentRow, 1, arrayEmployees[i].FirstName, borderedCellStyle);
                CreateCell(currentRow, 2, arrayEmployees[i].Name, borderedCellStyle);
                CreateCell(currentRow, 3, arrayEmployees[i].LastName, borderedCellStyle);
                CreateCell(currentRow, 4, arrayEmployees[i].DepartmentName, borderedCellStyle);
                CreateCell(currentRow, 5, arrayEmployees[i].PositionName, borderedCellStyle);
            }

             // Auto sized all the affected columns
            int lastColumNum = sheet.GetRow(0).LastCellNum;
            for (var i = 0; i <= lastColumNum; i++)
            {
                sheet.AutoSizeColumn(i);
                GC.Collect();
            }
            // Write Excel to disk 
            using var fileData = new FileStream($"{reportName}.xls", FileMode.Create);
            workbook.Write(fileData);
    }
}