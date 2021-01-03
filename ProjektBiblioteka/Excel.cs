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
    public class Report
    {
        public int idEgzemplarza { get; set; }
        public string tytulKsiazki { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{idEgzemplarza} {Count}";
        }
    }

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

        private void CreateSpreedsheet(List<Report> lists)
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
                foreach (var item in lists.OrderByDescending(x=>x.Count))
                {
                    bestBookWorksheet.Cells["A" + currentRow.ToString()].Value = item.idEgzemplarza;
                    bestBookWorksheet.Cells["B" + currentRow.ToString()].Value = item.Count;
                    currentRow++;
                }
               
                //var ksiazkaInformacje = from ks in context.Ksiazki
                //                        join eg in context.Egzemplarze on ks.idKsiazki equals eg.idKsiazki
                //                        join wyp in context.Wypozyczenia on eg.idEgzemplarza equals wyp.idEgzemplarza
                //                        select new { ks.idKsiazki, ks.rodzajKsiazki, ks.tytulKsiazki, wyp.dataWypozyczenia, wyp.dataZwrotu, wyp.idWypozyczenia, count = wyp };
                //ksiazkaInformacje.ToList();
                //List<string> list = new List<string>();
                //foreach (var item in ksiazkaInformacje)
                //{
                //    list.Add(item.ToString());
                //}
                //int row = 2;
                //foreach (var item in lists)
                //{
                //    bestBookWorksheet.Cells["B" + row.ToString()].Value = list;
                //    row++;
                //}
                //zapis
                package.SaveAs(spreadsheetInfo);
            }


            //populate

        }

        private List<Report> TheMostPopularBook()
        {
            //nowe
            List<int> temp2 = new List<int>();
            foreach (var line in context.Wypozyczenia.GroupBy(info => info.idEgzemplarza)
                        .Select(group => new
                        {
                            IdEgzemplarza = group.Key,
                            Count = group.Count()
                        })
                        .OrderByDescending(x => x.Count))
            {
                temp2.Add(line.IdEgzemplarza);
                temp2.Add(line.Count);
            }

         
               

            var query=context.Wypozyczenia.GroupBy(info => info.idEgzemplarza)
                        .Select(group => new
                        {
                            IdEgzemplarza = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Count);

            var listaRaportu = new List<Report>();
         
            foreach (var item in query)
            {
               listaRaportu.Add(new Report() { idEgzemplarza = item.IdEgzemplarza, Count = item.Count });
            
            }
            //


            return listaRaportu;


            //    //var c = from e in context.Ksiazki join ek in context.Egzemplarze on e.idKsiazki equals ek.idKsiazki orderby e.tytulKsiazki select new { ek.Ksiazki.tytulKsiazki, ek.idEgzemplarza };
            //    //c.GroupBy(x => new { x.tytulKsiazki, x.idEgzemplarza }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() });
            //    var query2 = context.Wypozyczenia.Select(x => new { Count = context.Wypozyczenia.Select(w => w.idWypozyczenia).Distinct().Count() }).OrderByDescending(y => y.Count).ToList();
            //var lista3 = new List<int>();
            //var lista = new List<Report>();

            //var query = from e in context.Egzemplarze
            //            join wyp in context.Wypozyczenia on e.idEgzemplarza equals wyp.idEgzemplarza
            //            select new { e.idEgzemplarza, count = e.idEgzemplarza };
            //List<string> nowa = new List<string>();
            //foreach (var item in query)
            //{
            //    nowa.Add(item.idEgzemplarza.ToString() + item.count.ToString());
            //}
            //foreach (var item in nowa)
            //{
            //    MessageBox.Show(item);
            //}      

            //foreach (var item in query2)
            //{
            //    lista3.Add(item.Count);
            //}



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

