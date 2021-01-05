using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
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



                foreach (var item in package.Workbook.Worksheets)
                {
                    Headers(item);
                }

                #region Populate Worksheet
                //populate
                
                int currentRowJanuary = 2;
                int currentRowFebruary = 2;
                int currentRowMarch = 2;
                int currentRowApril = 2;
                int currentRowMay = 2; int currentRowJune = 2;
                int currentRowJuly = 2;
                int currentRowAugust = 2;
                int currentRowSeptember = 2;
                int currentRowOctober = 2;
                int currentRowNovember = 2;
                int currentRowDecember = 2;

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
                foreach (var item in lists.OrderByDescending(x => x.Count).OrderByDescending(x=>x.dataWypozyczenia))
                {
                    switch (item.dataWypozyczenia.Month)
                    {
                        case 1:
                            bestBookJanuary.Cells["A" + currentRowJanuary.ToString()].Value = item.tytulKsiazki;
                            bestBookJanuary.Cells["B" + currentRowJanuary.ToString()].Value = item.Count;
                            bestBookJanuary.Cells["C" + currentRowJanuary.ToString()].Value = item.dataWypozyczenia;
                            bestBookJanuary.Cells["C2:C" + currentRowJanuary].Style.Numberformat.Format = "mm-yyyy";


                            currentRowJanuary++;
                            break;
                        case 2:
                            bestBookFebruary.Cells["A" + bestBookFebruary.ToString()].Value = item.tytulKsiazki;
                            bestBookFebruary.Cells["B" + bestBookFebruary.ToString()].Value = item.Count;
                            bestBookFebruary.Cells["C" + bestBookFebruary.ToString()].Value = item.dataWypozyczenia;
                            currentRowFebruary++;
                            bestBookFebruary.Cells["C2:C" + bestBookFebruary].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 3:
                            bestBookMarch.Cells["A" + bestBookMarch.ToString()].Value = item.tytulKsiazki;
                            bestBookMarch.Cells["B" + bestBookMarch.ToString()].Value = item.Count;
                            bestBookMarch.Cells["C" + bestBookMarch.ToString()].Value = item.dataWypozyczenia;
                            currentRowMarch++;
                            bestBookMarch.Cells["C2:C" + bestBookMarch].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 4:
                            bestBookApril.Cells["A" + bestBookApril.ToString()].Value = item.tytulKsiazki;
                            bestBookApril.Cells["B" + bestBookApril.ToString()].Value = item.Count;
                            bestBookApril.Cells["C" + bestBookApril.ToString()].Value = item.dataWypozyczenia;
                            currentRowApril++;
                            bestBookApril.Cells["C2:C" + bestBookApril].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 5:
                            bestBookMay.Cells["A" + bestBookMay.ToString()].Value = item.tytulKsiazki;
                            bestBookMay.Cells["B" + bestBookMay.ToString()].Value = item.Count;
                            bestBookMay.Cells["C" + bestBookMay.ToString()].Value = item.dataWypozyczenia;
                            currentRowMay++;
                            bestBookMay.Cells["C2:C" + bestBookMay].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 6:
                            bestBookJune.Cells["A" + currentRowJune.ToString()].Value = item.tytulKsiazki;
                            bestBookJune.Cells["B" + currentRowJune.ToString()].Value = item.Count;
                            bestBookJune.Cells["C" + currentRowJune.ToString()].Value = item.dataWypozyczenia;
                            currentRowJune++;
                            bestBookJune.Cells["C2:C" + currentRowJune].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 7:
                            bestBookJuly.Cells["A" + currentRowJuly.ToString()].Value = item.tytulKsiazki;
                            bestBookJuly.Cells["B" + currentRowJuly.ToString()].Value = item.Count;
                            bestBookJuly.Cells["C" + currentRowJuly.ToString()].Value = item.dataWypozyczenia;
                            bestBookJuly.Cells["C2:C" + currentRowJuly].Style.Numberformat.Format = "mm-yyyy";


                            currentRowJuly++;
                            break;
                        case 8:
                            bestBookAugust.Cells["A" + currentRowAugust.ToString()].Value = item.tytulKsiazki;
                            bestBookAugust.Cells["B" + currentRowAugust.ToString()].Value = item.Count;
                            bestBookAugust.Cells["C" + currentRowAugust.ToString()].Value = item.dataWypozyczenia;
                            currentRowAugust++;
                            bestBookAugust.Cells["C2:C" + currentRowAugust].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 9:
                            bestBookSeptember.Cells["A" + currentRowSeptember.ToString()].Value = item.tytulKsiazki;
                            bestBookSeptember.Cells["B" + currentRowSeptember.ToString()].Value = item.Count;
                            bestBookSeptember.Cells["C" + currentRowSeptember.ToString()].Value = item.dataWypozyczenia;
                            currentRowSeptember++;
                            bestBookSeptember.Cells["C2:C" + currentRowSeptember].Style.Numberformat.Format = "mm-yyyy";

                            break;
                        case 10:
                            bestBookOctober.Cells["A" + currentRowOctober.ToString()].Value = item.tytulKsiazki;
                            bestBookOctober.Cells["B" + currentRowOctober.ToString()].Value = item.Count;
                            bestBookOctober.Cells["C" + currentRowOctober.ToString()].Value = item.dataWypozyczenia;
                            currentRowOctober++;
                            bestBookOctober.Cells["C2:C" + currentRowOctober].Style.Numberformat.Format = "mm-yyyy";
                            break;
                        case 11:
                            bestBookNovember.Cells["A" + currentRowNovember.ToString()].Value = item.tytulKsiazki;
                            bestBookNovember.Cells["B" + currentRowNovember.ToString()].Value = item.Count;
                            bestBookNovember.Cells["C" + currentRowNovember.ToString()].Value = item.dataWypozyczenia;
                            currentRowNovember++;
                            bestBookNovember.Cells["C2:C" + currentRowNovember].Style.Numberformat.Format = "mm-yyyy";

                            break;
                        case 12:
                            bestBookDecember.Cells["A" + currentRowDecember.ToString()].Value = item.tytulKsiazki;
                            bestBookDecember.Cells["B" + currentRowDecember.ToString()].Value = item.Count;
                            bestBookDecember.Cells["C" + currentRowDecember.ToString()].Value = item.dataWypozyczenia;

                            currentRowDecember++;
                            bestBookDecember.Cells["C2:C" + currentRowDecember].Style.Numberformat.Format = "mm-yyyy";

                            break;
                    }
                    
                }

                int currentRow = 2;
                foreach (var item in listaRaportu.OrderByDescending(x=>x.Count))
                {
                    bestBookWorksheet.Cells["A" + currentRow.ToString()].Value = item.tytulKsiazki;
                    bestBookWorksheet.Cells["B" + currentRow.ToString()].Value = item.Count;
                    
                    currentRow++;
                   

                }
               
                bestBookWorksheet.Column(3).Hidden = true;


               // create a new piechart of type Line
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


             //   add the ranges to the chart
            //    set the names of the legend
                 lineChart.Series[0].Header = bestBookWorksheet.Cells["A1"].Value.ToString();
                lineChart.Series[1].Header = bestBookWorksheet.Cells["B1"].Value.ToString();
                // lineChart.Series[2].Header = bestBookWorksheet.Cells["C1"].Value.ToString();

                //position of the legend
                lineChart.Legend.Position = eLegendPosition.Right;

                //size of the chart
                lineChart.SetSize(600, 300);

                //add the chart at cell B6
                lineChart.SetPosition(8, 0, 1, 0);


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

