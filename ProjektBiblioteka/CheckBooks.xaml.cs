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
        string wynik = "";
        
        List<string> bookList = new List<string>();
        List<string> authors = new List<string>();
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();
        public CheckBooks()
        {

            InitializeComponent();
            //libraryEntitiesDataSet context = new libraryEntitiesDataSet();
            ShowAllTitles();
           


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
            //var qpc = (from res in
            //          (from c in context.Ksiazki
            //           join u in context.Ksiazki.GetTworcy()
            //           on c equals u.idKsiazki
            //           select u.PersonCompanyId)
            //           join u in GetUser()
            //          on res equals u.UserId
            //           select u).AsQueryable();

            //var q = from k in context.Ksiazki from t in Tworcy ;

            List<string> l = new List<string>();
            
            wynik = Authors.SelectedItem.ToString();
            var autor = wynik;

            int idTworcy = 1;
            int idKsiazki = 1;
            var AuthorIdQuery = context.Tworcy.Where(x => x.imieTworcy + x.nazwiskoTworcy == autor).Select(x => x.idTworcy);
            var query2 = context.Tworcy.SelectMany(x => x.Ksiazki, (x, ksiazki) => new { x.idTworcy, ksiazki = ksiazki.idKsiazki }).Where(x => x.idTworcy == AuthorIdQuery.FirstOrDefault()).ToList();
            var query1 = context.Tworcy;
            var query = from k in context.Ksiazki select k.Tworcy;
            string s = "";
            foreach (var item in query)
            {
                Console.WriteLine(item);
                s += item.ToString();
            }

            foreach (var item in query2)
            {

                idTworcy = item.idTworcy;
                idKsiazki = item.ksiazki;
              
            }

           // var result = context.Ksiazki.Where(x => x.idKsiazki == idKsiazki).ToList();
            var c = from e in context.Ksiazki where e.idKsiazki==idKsiazki join ek in context.Egzemplarze on e.idKsiazki equals ek.idKsiazki orderby e.tytulKsiazki select new { ek.Ksiazki.tytulKsiazki, ek.idEgzemplarza };
            c.GroupBy(x => new { x.tytulKsiazki, x.idEgzemplarza }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() });
            foreach (var item in c.GroupBy(x => new { x.tytulKsiazki }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() }))
            {
                l.Add($"\nTitle: \"{ item.tytulKsiazki}\" \nWe have: {item.MyCount} examples\n");
            }
            

            
            booksList.ItemsSource = l;
           
            //var query = from ks in context.Ksgitiazki join tw in context.Tworcy on new { ks.idKsiazki, ks.Tworcy. } equals new { tw.Ksiazki, tw.idTworcy } select x => x;

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
            ShowTitlesFromAuthors();
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
