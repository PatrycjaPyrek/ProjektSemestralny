using System;

namespace ProjektBiblioteka
{
    internal class DateConv
    {
        private DateTime dataWypozyczenia;
        private DateTime dataZwrotu;
        public DateTime DataWypozyczenia => dataWypozyczenia;
        public DateTime DataZwrotu => dataZwrotu;
       // int ileDni;
        public int IleDni => Zwroc(dataZwrotu-DataWypozyczenia);

        public DateConv(DateTime dataWypozyczenia, DateTime dataZwrotu)
        {
           this.dataWypozyczenia = dataWypozyczenia;
           this.dataZwrotu = dataZwrotu;
           Zwroc(this.dataZwrotu - this.dataWypozyczenia);
        }

        private int Zwroc(TimeSpan timeSpan) => (int)timeSpan.TotalDays;
    }
}