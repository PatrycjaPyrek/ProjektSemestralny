using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjektBiblioteka
{
    /// <summary>
    /// Interaction logic for BorrowBook.xaml
    /// </summary>
    public partial class ReturnBook : Window
    {
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();

        public ReturnBook()
        {
            InitializeComponent();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();



        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //code
            //var godz = DateTime.Now.Hour;
            //var minuta = DateTime.Now.Minute;
            //var sekunda = DateTime.Now.Second;
            //var data = DateTime.Now.Date;
            string dataigodzina = $"{DateTime.Now.ToLongDateString()}\t{ DateTime.Now.Hour:00}:{ DateTime.Now.Minute:00}:{DateTime.Now.Second:00}";
            dateAndTime_label.Content = dataigodzina;
            CommandManager.InvalidateRequerySuggested();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CheckBooks check = new CheckBooks();
            check.Show();
            this.Close();

        }

        private void zwrot(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitBorrow_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (var item in context.Wypozyczenia.Select(x => x.idWypozyczenia))
            {
                if (i == item)
                {
                    i++;
                }
            }

            int idEgzemplarzaWybranego = Convert.ToInt32(BookId.Text);
            int idKlientaWybranego = Convert.ToInt32(LibraryId.Text);
            foreach (var item in context.Wypozyczenia.Where(x => x.idEgzemplarza == idEgzemplarzaWybranego && x.idKlienta == idKlientaWybranego && x.dataZwrotu == null))
            {
                item.dataZwrotu = DateTime.Now.Date;
            }
            context.SaveChanges();

            List<string> klientInfo = new List<string>();
            List<string> ksiazkaInfo = new List<string>();

            //==================================Klienci info===========================================
            var klientInformacje = from k in context.Klienci where k.idKlienta == idKlientaWybranego join w in context.Wypozyczenia on k.idKlienta equals w.idKlienta select new { k.idKlienta, k.imieKlienta, k.nazwiskoKlienta };
            klientInformacje.ToList();
            foreach (var item in klientInformacje)
            {
                klientInfo.Add(item.idKlienta.ToString());
                klientInfo.Add(item.imieKlienta.ToString());
                klientInfo.Add(item.nazwiskoKlienta.ToString());
            }
            LibraryReturnedId.Content = klientInfo[0];
            NameReturned.Content = klientInfo[1] + " " + klientInfo[2];
            //==================================Ksiazki Info===========================================
            var ksiazkaInformacje = from ks in context.Ksiazki
                                    join eg in context.Egzemplarze on ks.idKsiazki equals eg.idKsiazki
                                    where eg.idEgzemplarza == idEgzemplarzaWybranego
                                    join wyp in context.Wypozyczenia on eg.idEgzemplarza equals wyp.idEgzemplarza
                                    select new { ks.idKsiazki, ks.rodzajKsiazki, ks.tytulKsiazki, wyp.dataWypozyczenia, wyp.dataZwrotu, wyp.idWypozyczenia };
            ksiazkaInformacje.ToList();
            int idWypozyczeniaWybranego=0;
            foreach (var item in ksiazkaInformacje)
            {
                ksiazkaInfo.Add(item.idKsiazki.ToString());
                ksiazkaInfo.Add(item.tytulKsiazki.ToString());
                ksiazkaInfo.Add(item.rodzajKsiazki.ToString());
                ksiazkaInfo.Add(item.dataWypozyczenia.Date.ToString());
                ksiazkaInfo.Add(DateTime.Now.Date.ToString());
         
                idWypozyczeniaWybranego = item.idWypozyczenia;
            }
            BookIdBorrowed.Content = ksiazkaInfo[0];
            TitleBorrowed.Content = ksiazkaInfo[1];
            TypeBorrowed.Content = ksiazkaInfo[2];

            DateBorrowed.Content = ksiazkaInfo[3].Replace("00:00:00","") + " - " + ksiazkaInfo[4].Replace("00:00:00","");
            int ile = 0;
            //=======================Doplaty====================================
            //Doplata liczona jesli ktos przetrzyma ksiazke (wiecej niz 30 dni!)
            foreach (var item in ksiazkaInformacje.Select(x => new { x.idWypozyczenia, x.dataWypozyczenia, x.dataZwrotu, x.rodzajKsiazki }).Where(x => DbFunctions.DiffDays(x.dataWypozyczenia, x.dataZwrotu) > 30))
            {
                DateConv dateC = new DateConv(item.dataWypozyczenia, DateTime.Now);
                 ile=dateC.IleDni-30;
                
                var doplataZaRodzaj = context.Cennik.Where(x => x.rodzajKsiazki == item.rodzajKsiazki).Select(x => x.oplataZa7Dni).FirstOrDefault();
                decimal doZaplatyObliczone = (doplataZaRodzaj / 7) * ile;
                //tworze nowa doplate na podstawie wyzej obliczonych danych
                var doplataNowa = new Doplaty()
                {
                    idWypozyczenia = idWypozyczeniaWybranego,
                    doplata = doZaplatyObliczone

                };
                context.Doplaty.Add(doplataNowa);
               

            }
            context.SaveChanges();

            var czyJestDoplata = context.Doplaty.Where(x => x.idWypozyczenia == idWypozyczeniaWybranego).Select(x => x.doplata.Value);
            foreach (var item in context.Doplaty.Where(x=>x.idWypozyczenia==idWypozyczeniaWybranego).Select(x=>x.doplata))
            {
               
                ToPay.Content = item.Value.ToString();
            }
           
           

            ksiazkaInfo.Clear();



        

            



}

        }
    }

