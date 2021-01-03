using System;
using System.Collections.Generic;
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
    public partial class BorrowBook : Window
    {
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();

        public BorrowBook()
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
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitBorrow_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
           
            bool wypozyczono = true;
            int idEgzemplarzaWybranego = Convert.ToInt32(BookId.Text);
            int idKlientaWybranego = Convert.ToInt32(LibraryId.Text);
            List<string> klientInfo = new List<string>();
            List<string> ksiazkaInfo = new List<string>();
            //=============o kliencie ==============//
            
           

            var ksiazkaInformacje = from ks in context.Ksiazki
                                    join eg in context.Egzemplarze on ks.idKsiazki equals eg.idKsiazki
                                    where eg.idEgzemplarza == idEgzemplarzaWybranego
                                    join c in context.Cennik on ks.rodzajKsiazki equals c.rodzajKsiazki
                                    select new { ks.idKsiazki, ks.rodzajKsiazki, ks.tytulKsiazki,c.oplataZa7Dni };
            ksiazkaInformacje.ToList();
            decimal oplataZa7Dni = 0;
            foreach (var item in ksiazkaInformacje)
            {
                ksiazkaInfo.Add(item.idKsiazki.ToString());
                ksiazkaInfo.Add(item.tytulKsiazki.ToString());
                ksiazkaInfo.Add(item.rodzajKsiazki.ToString());
                ksiazkaInfo.Add(item.oplataZa7Dni.ToString());
                oplataZa7Dni = item.oplataZa7Dni;
            }
            foreach (var item in context.Wypozyczenia.Select(x => x.idWypozyczenia))
            {
                if (i == item)
                {
                    i=item+1;
                }
            }

            var wypozyczenie = new Wypozyczenia()
            {
                idWypozyczenia = i,
                idEgzemplarza = idEgzemplarzaWybranego,
                idKlienta = idKlientaWybranego,
                dataWypozyczenia = DateTime.Now.Date,
                oplataZa7Dni = oplataZa7Dni
            }; 

            foreach (var item in context.Egzemplarze.Where(x=>x.idEgzemplarza==idEgzemplarzaWybranego))
            {
                foreach (var wypozyczenieZ in context.Wypozyczenia.Select(x=>new { x.idEgzemplarza, x.dataZwrotu }))
                    {
                    if (wypozyczenieZ.dataZwrotu == null && wypozyczenieZ.idEgzemplarza==idEgzemplarzaWybranego)
                    {
                        wypozyczono = false;
                        MessageBox.Show("Ten egzemplarz jest obecnie niedostępny!");
                        break;
                    }
                 
                }
                if (wypozyczono == true)
                {

                  
                    context.Wypozyczenia.Add(wypozyczenie);
                    wypozyczono = true;
                    MessageBox.Show("Wypożyczono");
                }
                
            }
            context.SaveChanges();
            if (wypozyczono == true)
            {


                //==================================Klienci info===========================================
                var klientInformacje = from k in context.Klienci where k.idKlienta == idKlientaWybranego join w in context.Wypozyczenia on k.idKlienta equals w.idKlienta select new { k.idKlienta, k.imieKlienta, k.nazwiskoKlienta };
                klientInformacje.ToList();
                foreach (var item in klientInformacje)
                {
                    klientInfo.Add(item.idKlienta.ToString());
                    klientInfo.Add(item.imieKlienta.ToString());
                    klientInfo.Add(item.nazwiskoKlienta.ToString());
                }
                LibraryIdBorrowed.Content = klientInfo[0];
                NameBorrowed.Content = klientInfo[1] + " " + klientInfo[2];
                //==================================Ksiazki Info===========================================
                var result = from ks in context.Ksiazki
                                        join eg in context.Egzemplarze on ks.idKsiazki equals eg.idKsiazki
                                        where eg.idEgzemplarza == idEgzemplarzaWybranego
                                        join wyp in context.Wypozyczenia on eg.idEgzemplarza equals wyp.idEgzemplarza
                                        join c in context.Cennik on ks.rodzajKsiazki equals c.rodzajKsiazki
                                        select new { ks.idKsiazki, ks.rodzajKsiazki, ks.tytulKsiazki, c.oplataZa7Dni };
               
                foreach (var item in ksiazkaInformacje)
                {
                    ksiazkaInfo.Add(item.idKsiazki.ToString());
                    ksiazkaInfo.Add(item.tytulKsiazki.ToString());
                    ksiazkaInfo.Add(item.rodzajKsiazki.ToString());
                    ksiazkaInfo.Add(item.oplataZa7Dni.ToString());
                    oplataZa7Dni = item.oplataZa7Dni;
                }
                BookIdBorrowed.Content = ksiazkaInfo[0];
                TitleBorrowed.Content = ksiazkaInfo[1];
                TypeBorrowed.Content = ksiazkaInfo[2];
                DateBorrowed.Content = wypozyczenie.dataWypozyczenia;

                ksiazkaInfo.Clear();
                


            }
            
            //foreach (var item in context.Egzemplarze.Where(x => x.idEgzemplarza == idEgzemplarzaWybranego))
            //{
            //    MessageBox.Show(item.GetState().ToString());
            //    if (item.GetState() == Available.not_available)
            //    {
            //        MessageBox.Show("Ten egzemplarz jest obecnie niedostępny!");
            //    }
            //    else
            //    {
            //        context.Wypozyczenia.Add(wypozyczenie);

            //        MessageBox.Show($"Borrowed");
            //        foreach (var wypozyczonyEgzemplarz in context.Egzemplarze.Where(x => x.idEgzemplarza == idEgzemplarzaWybranego))
            //        {
            //            wypozyczonyEgzemplarz.Counter += 1;
            //            wypozyczonyEgzemplarz.Borrowed();
            //            MessageBox.Show(wypozyczonyEgzemplarz.GetState().ToString());
            //        }
            //    }

            //}
            //context.SaveChanges();




        }

        private void UserButton_Checked(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }
    }
}
