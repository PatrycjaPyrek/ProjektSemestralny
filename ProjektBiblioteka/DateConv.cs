using System;

namespace ProjektBiblioteka
{
    internal class DateConv
    {
        private int ileDni;

       // int ileDni;
        public int IleDni => ileDni;

        public DateConv(DateTime dataWypozyczenia, DateTime dataZwrotu)
        {

            TimeSpan roznica = dataWypozyczenia - dataZwrotu;
            ileDni = (int)roznica.TotalDays;
            //Zwroc(this.dataZwrotu - this.dataWypozyczenia);
        }

       // private int Zwroc(TimeSpan timeSpan) => (int)timeSpan.TotalDays;
    }
}