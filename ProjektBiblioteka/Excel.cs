using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using ProjektBiblioteka.baza;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
                List<ExcelWorksheet> zbior = new List<ExcelWorksheet>();
                
                foreach (var item in DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12))
                {
                    
                    var najlepszaKsiazkaMiesiaca = package.Workbook.Worksheets.Add(item);
                    zbior.Add(najlepszaKsiazkaMiesiaca);
                }
                



                foreach (var item in package.Workbook.Worksheets)
                {
                    Headers(item);
                }

                #region Populate Worksheet
                //populate
                
             
                List<int> aktualnyWiersz = new List<int>();
                for (int i = 0; i < 12; i++)
                {
                    aktualnyWiersz.Add(2);
                }

                //wszystkie ksiazki 
                var listaRaportu = new List<Report>();
               
                var noweQuery = from k in context.Ksiazki
                                join e in context.Egzemplarze on k.idKsiazki equals e.idKsiazki
                                join w in context.Wypozyczenia on e.idEgzemplarza equals w.idEgzemplarza
                                group k by new { k.tytulKsiazki } into g
                                select new
                                {
                                    TytulKsiazki = g.Key.tytulKsiazki,
                                    Count = g.Count(),
                                };
                foreach (var item in noweQuery)
                {

                    listaRaportu.Add(new Report() { tytulKsiazki = item.TytulKsiazki, Count = item.Count });
                }



                //sortowane po count malejaco i po dacie malejaco
                foreach (var item in lists.OrderByDescending(x => x.Count).OrderByDescending(x => x.dataWypozyczenia))
                {

                    int i = item.dataWypozyczenia.Month-1;
                        int wiersz = aktualnyWiersz[i];
                        zbior[i].Cells["A" + wiersz].Value = item.tytulKsiazki;
                        zbior[i].Cells["B" + wiersz].Value = item.Count;
                        zbior[i].Cells["C" + wiersz].Value = item.dataWypozyczenia;
                        zbior[i].Cells["C2:C" + wiersz].Style.Numberformat.Format = "mmm-yyyy";
                        aktualnyWiersz[i]++;
                    

                }


                int currentRow = 2;
                foreach (var item in listaRaportu.OrderByDescending(x=>x.Count))
                {
                    bestBookWorksheet.Cells["A" + currentRow.ToString()].Value = item.tytulKsiazki;
                    bestBookWorksheet.Cells["B" + currentRow.ToString()].Value = item.Count;
                    
                    currentRow++;
                   

                }
               
                bestBookWorksheet.Column(3).Hidden = true;

                //chart
               //// create a new piechart of type Line
               // ExcelLineChart lineChart = bestBookWorksheet.Drawings.AddChart("lineChart", eChartType.Line) as ExcelLineChart;

               // //set the title
               // lineChart.Title.Text = "Best Book chart";

               // //create the ranges for the chart
               // var rangeLabel = bestBookWorksheet.Cells["A1:B1"];
               // for (int i = 2; i < lists.Count; i++)
               // {
               //     var range = bestBookWorksheet.Cells[$"A{i}:B{i}"];
               //     lineChart.Series.Add(range, rangeLabel);
               //     i++;

               // }


               // //   add the ranges to the chart
               // //    set the names of the legend
              
               //  lineChart.Series[0].Header = bestBookWorksheet.Cells["A1"].Value.ToString();
               // lineChart.Series[1].Header = bestBookWorksheet.Cells["B1"].Value.ToString();
               // // lineChart.Series[2].Header = bestBookWorksheet.Cells["C1"].Value.ToString();

               // //position of the legend
               // lineChart.Legend.Position = eLegendPosition.Right;

               // //size of the chart
               // lineChart.SetSize(600, 300);

               // //add the chart at cell B6
               // lineChart.SetPosition(8, 0, 1, 0);


                #endregion
                package.SaveAs(spreadsheetInfo);
            }
        }

        private static void Headers(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1"].Value = "Id";
            worksheet.Cells["B1"].Value = "OrderCount";
            worksheet.Cells["C1"].Value = "Date (month-year)";
            worksheet.Cells["A1:C1"].Style.Font.Bold = true;
            worksheet.Cells["A1:C1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Column(3).Width = 20;

            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 15;
          

        }



        private List<Report> TheMostPopularBook()
        {

            var listaRaportu = new List<Report>();
            //zapytanie SQL do ktorego dazymy:
            //  select Month(w.dataWypozyczenia) as MiesiacWypozyczenia,k.tytulKsiazki, count(w.idEgzemplarza)
            //as WypozyczeniaIlosciowo from Wypozyczenia w join Egzemplarze e on e.idEgzemplarza=w.idEgzemplarza join Ksiazki k on e.idKsiazki=k.idKsiazki group by k.tytulKsiazki,Month(w.dataWypozyczenia);
            var noweQuery = from k in context.Ksiazki
                            join e in context.Egzemplarze on k.idKsiazki equals e.idKsiazki
                            join w in context.Wypozyczenia on e.idEgzemplarza equals w.idEgzemplarza
                            group k by new {  w.dataWypozyczenia, k.tytulKsiazki } into g
                            select new
                            {
                                data = g.Key.dataWypozyczenia,
                                TytulKsiazki = g.Key.tytulKsiazki,
                                Count = g.Count(),
                            };
            foreach (var item in noweQuery)
            {

                listaRaportu.Add(new Report() { tytulKsiazki = item.TytulKsiazki, Count = item.Count,dataWypozyczenia=item.data });
            }
            return listaRaportu;
           



        }
    }
 
}

