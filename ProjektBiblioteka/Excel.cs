using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBiblioteka
{
    class Excel
    {
        public Excel()
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Month report");
               
                var excelWorksheet = excel.Workbook.Worksheets["Worksheet1"];
                var headerRow = new List<string[]>() {
                new string[] { "Book id", "Book title", "How many times borrowed", "Data wypożyczenia" } };
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + 1;
                var worksheet = excel.Workbook.Worksheets["Worksheet1"];

                // Popular header row data
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                FileInfo excelFile = new FileInfo(@"C:\Użytkownicy\test.xlsx");
                excel.SaveAs(excelFile);
             

            }
            
        }
        }
    }

