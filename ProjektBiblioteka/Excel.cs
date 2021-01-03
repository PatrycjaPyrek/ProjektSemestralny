using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
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
        public DateTime dataWypozyczenia { get; set; }
        public DateTime dataZwrotu { get; set; }

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
                var bestBookWorksheet = package.Workbook.Worksheets.Add("BestBook (ALL)");
                var bestBookJanuary = package.Workbook.Worksheets.Add("January");
                var bestBookFebruary = package.Workbook.Worksheets.Add("February");
                var bestBookMarch = package.Workbook.Worksheets.Add("March");
                var bestBookApril = package.Workbook.Worksheets.Add("April");
                var bestBookMay = package.Workbook.Worksheets.Add("May");
                var bestBookJune = package.Workbook.Worksheets.Add("June");
                var bestBookJuly = package.Workbook.Worksheets.Add("July");
                var bestBookAugust = package.Workbook.Worksheets.Add("August");
                var bestBookSeptember = package.Workbook.Worksheets.Add("September");
                var bestBookOctober = package.Workbook.Worksheets.Add("October");
                var bestBookNovember = package.Workbook.Worksheets.Add("November");
                var bestBookDecember = package.Workbook.Worksheets.Add("December");
                bestBookWorksheet.Cells["A1"].Value = "Id";
                bestBookWorksheet.Cells["B1"].Value = "OrderCount";
                bestBookWorksheet.Cells["A1:B1"].Style.Font.Bold = true;
                bestBookWorksheet.Cells["A1:B1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                bestBookWorksheet.Column(1).Width = 20;
                bestBookWorksheet.Column(2).Width = 15;

                //populate
                int currentRow = 2;
                foreach (var item in lists.OrderByDescending(x=>x.Count))
                {
                    bestBookWorksheet.Cells["A" + currentRow.ToString()].Value = item.tytulKsiazki;
                    bestBookWorksheet.Cells["B" + currentRow.ToString()].Value = item.Count;
                    currentRow++;
                }
                //Jan
                currentRow = 2;
                foreach (var item in lists.OrderByDescending(x => x.Count))
                {
                    bestBookWorksheet.Cells["A" + currentRow.ToString()].Value = item.tytulKsiazki;
                    bestBookWorksheet.Cells["B" + currentRow.ToString()].Value = item.Count;
                    currentRow++;
                }


                //create a new piechart of type Line
                ExcelLineChart lineChart = bestBookWorksheet.Drawings.AddChart("lineChart", eChartType.Line) as ExcelLineChart;

                //set the title
                lineChart.Title.Text = "Best Book chart";

                //create the ranges for the chart
                var rangeLabel = bestBookWorksheet.Cells["A1:B1"];
                for (int i = 2; i < lists.Count; i++)
                {
                    var range = bestBookWorksheet.Cells[$"A{i}:B{i}"];
                    lineChart.Series.Add(range, rangeLabel);
                    i++;
                  
                }

              
                //add the ranges to the chart
               
              

                //set the names of the legend
                lineChart.Series[0].Header = bestBookWorksheet.Cells["A2"].Value.ToString();
                lineChart.Series[1].Header = bestBookWorksheet.Cells["A3"].Value.ToString();

                //position of the legend
                lineChart.Legend.Position = eLegendPosition.Right;

                //size of the chart
                lineChart.SetSize(600, 300);

                //add the chart at cell B6
                lineChart.SetPosition(5, 0, 1, 0);




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
              // listaRaportu.Add(new Report() { idEgzemplarza = item.IdEgzemplarza, Count = item.Count });
            
            }
            //  select ks.tytulKsiazki,Count(ks.idKsiazki) as Ile from Wypozyczenia w join Egzemplarze
            // e on e.idEgzemplarza=w.idEgzemplarza join Ksiazki ks on ks.idKsiazki=e.idKsiazki  group by ks.idKsiazki,ks.tytulKsiazki;


            var noweQuery = from k in context.Ksiazki
                            join e in context.Egzemplarze on k.idKsiazki equals e.idKsiazki
                            join w in context.Wypozyczenia on e.idEgzemplarza equals w.idEgzemplarza

                            group k by k.tytulKsiazki into g
                            select new
                            {
                                TytulKsiazki = g.Key,
                                Count = g.Count(),
                            };
            foreach (var item in noweQuery)
            {
                listaRaportu.Add(new Report() { tytulKsiazki = item.TytulKsiazki, Count = item.Count });
            }
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

