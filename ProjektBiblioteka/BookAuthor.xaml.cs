using ProjektBiblioteka.baza;
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

namespace ProjektBiblioteka
{
    /// <summary>
    /// Interaction logic for BookAuthor.xaml
    /// </summary>
    public partial class BookAuthor : Window
    {
        libraryDataSet context = new libraryDataSet();
        public BookAuthor()
        {
            InitializeComponent();

           


        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            AuthorsList.ItemsSource = context.Tworcy.Select(x => x);
            booksList.ItemsSource = context.Ksiazki.Select(x => x);
            int idKsiazki = Convert.ToInt32(bookId);
            foreach (var item in context.Ksiazki.Where(x=>x.idKsiazki==idKsiazki))
            {
                Ksiazki ksiazka = new Ksiazki()
                {
                    idKsiazki = item.idKsiazki,
                    tytulKsiazki = item.tytulKsiazki,
                    rodzajKsiazki = item.rodzajKsiazki,
                    rokWydania = item.rokWydania,

                };
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
