using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektBiblioteka
{
    public class Excel
    {
       // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();
        private readonly ReportType reportType;
        public enum ReportType
        {
            TheMostPopularBook,
            AllBooks,
            BestClient,
            Income
        }
        public Excel(ReportType reportType)
        {
            if (reportType == ReportType.TheMostPopularBook)
            {
                CreateSpreedsheet(TheMostPopularBook());
                MessageBox.Show("DONE");
               
            }
            CreateSpreedsheet(TheMostPopularBook());

        }

        private void CreateSpreedsheet(List<int> lists)
        {
            string spreadsheetPath = "mostpopularbook.xlsx";
            File.Delete(spreadsheetPath);
            FileInfo spreadsheetInfo = new FileInfo(spreadsheetPath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(spreadsheetInfo))
            {
                var bestBookWorksheet = package.Workbook.Worksheets.Add("BestBook");
                bestBookWorksheet.Cells["A1"].Value = "Id";
                bestBookWorksheet.Cells["B1"].Value = "OrderCount";
                //populate
                int currentRow = 2;
                foreach (var item in lists)
                {
                    bestBookWorksheet.Cells["A" + currentRow.ToString()].Value = lists.Count;
                }
                package.SaveAs(spreadsheetInfo);
            }


            //populate

        }

        private List<int> TheMostPopularBook()
        {
            var query = context.Wypozyczenia.Select(x=>new{Count = context.Wypozyczenia.Select(w => w.idWypozyczenia).Distinct().Count()}).OrderByDescending(y => y.Count).ToList();
            var lista = new List<int>();
            foreach (var item in query)
            {
                lista.Add(item.Count);
            }
           
            return lista;
            
    }
    }
    //{
    //    public Excel()
    //    {
    //        using (ExcelPackage excel = new ExcelPackage())
    //        {
    //            excel.Workbook.Worksheets.Add("Month report");
               
    //            var excelWorksheet = excel.Workbook.Worksheets["Worksheet1"];
    //            var headerRow = new List<string[]>() {
    //            new string[] { "Book id", "Book title", "How many times borrowed", "Data wypożyczenia" } };
    //            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + 1;
    //            var worksheet = excel.Workbook.Worksheets["Worksheet1"];

    //            // Popular header row data
    //            worksheet.Cells[headerRange].LoadFromArrays(headerRow);

    //            FileInfo excelFile = new FileInfo("test.xlsx");
    //            excel.SaveAs(excelFile);
             

    //        }
            
        //}
        //}
    }

