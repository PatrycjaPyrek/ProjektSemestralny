﻿using System;
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitBorrow_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (var item in context.Wypozyczenia.Select(x=>x.idWypozyczenia))
            {
                if (i == item)
                {
                    i++;
                }
            }


            var wypozyczenie = new Wypozyczenia()
            {
                idWypozyczenia = i,
                idEgzemplarza = Convert.ToInt32(BookId.Text),
                idKlienta = Convert.ToInt32(LibraryId.Text),
                dataWypozyczenia = DateTime.Now.Date
                

            };
            context.Wypozyczenia.Add(wypozyczenie);
            context.SaveChanges();
            MessageBox.Show($"Borrowed");

        }
    }
}