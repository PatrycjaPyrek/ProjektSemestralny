using System;

namespace ProjektBiblioteka
{
    internal class DateConv
    {
        private DateTime dataWypozyczenia;
        private DateTime dataZwrotu;
        public DateTime DataWypozyczenia { get; set; }
        public DateTime DataZwrotu { get; set; }
        int ileDni;
        public int IleDni => Zwroc(dataWypozyczenia-DataZwrotu);

        public DateConv(DateTime dataWypozyczenia, DateTime dataZwrotu)
        {
           DataWypozyczenia = dataWypozyczenia;
           DataZwrotu = dataZwrotu;
           Zwroc(dataZwrotu - dataWypozyczenia);
        }

        private int Zwroc(TimeSpan timeSpan)
        {
            return timeSpan.Days;
        }
    }
}