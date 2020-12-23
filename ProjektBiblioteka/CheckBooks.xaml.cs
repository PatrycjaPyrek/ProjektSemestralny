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
    /// Interaction logic for CheckBooks.xaml
    /// </summary>
    public partial class CheckBooks : Window
    {
        List<string> bookList = new List<string>();
        List<string> authors = new List<string>();
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();
        public CheckBooks()
        {

            InitializeComponent();
            //libraryEntitiesDataSet context = new libraryEntitiesDataSet();
            ShowAllTitles();
            ShowTitlesFromAuthors();


            foreach (var item in context.Tworcy.Select(x=>x.imieTworcy+x.nazwiskoTworcy))
            {
                authors.Add(item);
            }
            Authors.ItemsSource = authors;

          
            for (int i = 0; i < booksList.Items.Count; i++)
            {
               

            }
        }

        private void ShowTitlesFromAuthors()
        {
            List<int> l = new List<int>();
            var query = from k in context.Ksiazki select k.Tworcy;
            string s = "";
            foreach (var item in query)
            {
                Console.WriteLine(item);
                s += item.ToString();
            }
            MessageBox.Show(s);
            //var query = from ks in context.Ksiazki join tw in context.Tworcy on new { ks.idKsiazki, ks.Tworcy. } equals new { tw.Ksiazki, tw.idTworcy } select x => x;

            // var x = from ks in context.Ksiazki join tw in context.Tworcy on ks.Tworcy.Select(x=>new { ks.idKsiazki)}) equals tw select x => x;
            // var c = from e in context.Ksiazki join ek in context.Egzemplarze on e.idKsiazki equals ek.idKsiazki join a in context.Tworcy on e.Tworcy equals KsiazkiTworcy orderby e.tytulKsiazki select new { ek.Ksiazki.tytulKsiazki, ek.idEgzemplarza };
            // c.GroupBy(x => new { x.tytulKsiazki, x.idEgzemplarza }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() });
        }

        /// <summary>
        /// Funkcja zwracająca tytuły wszystkich książek
        /// </summary>
        private void ShowAllTitles()
        {
            var c = from e in context.Ksiazki join ek in context.Egzemplarze on e.idKsiazki equals  ek.idKsiazki orderby e.tytulKsiazki select new { ek.Ksiazki.tytulKsiazki, ek.idEgzemplarza };
            c.GroupBy(x => new { x.tytulKsiazki, x.idEgzemplarza }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() });
            foreach (var item in c.GroupBy(x => new { x.tytulKsiazki }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() }))
            {
                bookList.Add($"\nTitle: \"{ item.tytulKsiazki}\" \nWe have: {item.MyCount} examples\n");
            }
            booksList.ItemsSource = bookList;
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void zwrot(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void checkByAuthor_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Zwraca tytuły wszystkich książek w bibliotece
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTitles_Click(object sender, RoutedEventArgs e)
        {
            bookList.Clear();
            ShowAllTitles();
            
        }
    }
    
}
