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
        List<string> genres = new List<string>();
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();
       
        
        

        private void booksList_Click(object sender, EventArgs e)
        {
          
            Ksiazki ksiazkaWybrana = new Ksiazki();
            Tworcy tworcaWybrany = new Tworcy();
            List<int> TworcyId = new List<int>();
            List<Tworcy> listaTworcow = new List<Tworcy>();
         
            string tylkoTytulKsiazki = "";
            string elementListy = booksList.SelectedItem.ToString().Replace("Title: \"", "").Replace("\"", "").Trim() ;
            int pozycjaNonwejLinii = elementListy.IndexOf("\n");
            if (pozycjaNonwejLinii >= 0)
            {
                 tylkoTytulKsiazki += elementListy.Substring(0, pozycjaNonwejLinii).Trim();
            }
            var queryIdKsiazki = context.Ksiazki.Where(x=>x.tytulKsiazki==tylkoTytulKsiazki).Select(x => x.idKsiazki);
            var query2 = context.Ksiazki.SelectMany(x => x.Tworcy, (x, tworcy2) => new { x.idKsiazki, tworcy = tworcy2.idTworcy }).Where(x => x.idKsiazki == queryIdKsiazki.FirstOrDefault()).ToList();
            var query1 = context.Ksiazki;
            var query = from k in context.Tworcy select k.Ksiazki;
            string s = "";
    







            // var AuthorIdQuery = context.Tworcy.Where(x => x.imieTworcy + " " + x.nazwiskoTworcy == autor).Select(x => x.idTworcy);
            //var query2 = context.Tworcy.SelectMany(x => x.Ksiazki, (x, ksiazki) => new { x.idTworcy, ksiazki = ksiazki.idKsiazki }).Where(x => x.idTworcy == AuthorIdQuery.FirstOrDefault()).ToList();
            var queryTworcy = context.Tworcy;

            var queryResult = from r in context.Tworcy select r.Ksiazki;
            string nowy = "";
            foreach (var item in queryResult)
            {
                nowy += item.ToString();
            }

            foreach (var item in context.Ksiazki.Where(x=>x.tytulKsiazki==tylkoTytulKsiazki).Select(x => x))
            {
                ksiazkaWybrana = item;
                
               // szczegolyKsiazki.Add(item);
            }
            Tworcy tworcy = new Tworcy();
            int idTworcy = 1;
            foreach (var item in query)
            {
                Console.WriteLine(item);
                s += item.ToString();
            }

            foreach (var item in query2)
            {

                idTworcy = item.tworcy;

                TworcyId.Add(idTworcy);

            }

            var autorzy = context.Tworcy.Where(t => TworcyId.Contains(t.idTworcy)).Select(x => x);

            foreach (var item in autorzy)
            {

                tworcaWybrany = item;
                listaTworcow.Add(item);
            }
            string listaTworcowString = "";
            foreach (var item in listaTworcow)
            {
                if (listaTworcow.Count >= 2)
                {
                    listaTworcowString += $"{item.imieTworcy} {item.nazwiskoTworcy}, ";
                }
                else
                {
                    listaTworcowString += item.imieTworcy+" "+item.nazwiskoTworcy;
                }
            }
            if (listaTworcowString[listaTworcowString.Length-2] == ',')
            {
                listaTworcowString=listaTworcowString.Substring(0,listaTworcowString.Length-2);
            }
          


            //  tworcy = queryTworcy;
            // ksiazkaWybrana.Tworcy = query2.;
            MessageBox.Show(ksiazkaWybrana+"\nAUTORZY: "+listaTworcowString);
           // MessageBox.Show(ksiazkaWybrana.ToString());
        }
        
        
        public CheckBooks()
        {

            InitializeComponent();
            //libraryEntitiesDataSet context = new libraryEntitiesDataSet();
            ShowAllTitles();
           


            foreach (var item in context.Tworcy.OrderBy(x => x.nazwiskoTworcy).Select(x=> new { x.imieTworcy,x.nazwiskoTworcy }))
            {
                authors.Add(item.imieTworcy+" "+item.nazwiskoTworcy);
            }
            Authors.ItemsSource = authors;

            foreach (var item in context.gatunki.Select(x=>x.gatunek).OrderBy(x=>x))
            {
                genres.Add(item);
            }
            Genres.ItemsSource = genres;



            for (int i = 0; i < booksList.Items.Count; i++)
            {


            }
        }


        /// <summary>
        /// Wypisuje tytuł książki i ilość egzemplarzy na podstawie wybranego autora
        /// </summary>
        private void ShowTitlesFromAuthors()
        {
            


            List<string> l = new List<string>();
            List<int> idKsiazek = new List<int>();
            if (Authors.SelectedItem == null)
            {
                MessageBox.Show("Musisz wybrać element!");
            }
            else { wynik = Authors.SelectedItem.ToString(); }
          
            var autor = wynik;

            int idTworcy = 1;
            int idKsiazki = 1;
            var AuthorIdQuery = context.Tworcy.Where(x => x.imieTworcy +" "+ x.nazwiskoTworcy == autor).Select(x => x.idTworcy);
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
                idKsiazek.Add(idKsiazki);
              
            }
            var ksiazkiAutorow = context.Ksiazki.Where(t => idKsiazek.Contains(t.idKsiazki)).Select(x => new { x.tytulKsiazki,x.idKsiazki }).ToList();

            foreach (var item in ksiazkiAutorow)
            {
                foreach (var i in context.Egzemplarze.Where(x=>x.idKsiazki==item.idKsiazki).GroupBy(x=>new { item.tytulKsiazki}).Select(g=>new { g.Key.tytulKsiazki,MyCount=g.Count()}))
                {
                   
                    l.Add($"Title: \"{ item.tytulKsiazki}\" \nWe have: {i.MyCount} examples\n");
                }
            }   

            booksList.ItemsSource = l ;
       
            wynik = "";
            
        }

        /// <summary>
        /// Funkcja zwracająca tytuły wszystkich książek
        /// </summary>
        private void ShowAllTitles()
        {

            var c = from e in context.Ksiazki join ek in context.Egzemplarze on e.idKsiazki equals ek.idKsiazki orderby e.tytulKsiazki select new { ek.Ksiazki.tytulKsiazki, ek.idEgzemplarza };
            c.GroupBy(x => new { x.tytulKsiazki, x.idEgzemplarza }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() });
            var noweQuery = from k in context.Ksiazki
                            join e in context.Egzemplarze on k.idKsiazki equals e.idKsiazki
                            join w in context.Wypozyczenia on e.idEgzemplarza equals w.idEgzemplarza
                            group k by new { w.dataZwrotu, k.tytulKsiazki } into g
                            select new
                            {
                                DataZwrotu = g.Key.dataZwrotu,
                                TytulKsiazki = g.Key.tytulKsiazki,
                                Count = g.Count(),
                            };
            var lista = noweQuery.ToList();
            var dictionary = new Dictionary<string, int>();

            foreach (var p in c.Select(y => y.tytulKsiazki))
            {


                foreach (var item in lista.Where(x => x.TytulKsiazki == p))
                {

                    if (item.DataZwrotu == null)
                    {
                        if (dictionary.ContainsKey(item.TytulKsiazki) == true)
                        {
                            dictionary[item.TytulKsiazki] -= 1;
                        }
                        else
                        {

                            dictionary.Add(item.TytulKsiazki, item.Count - 1);
                        }
                    }
                    else
                    {
                        if (dictionary.ContainsKey(item.TytulKsiazki) == true)
                        {
                            dictionary[item.TytulKsiazki] += 1;
                        }
                        else
                        {

                            dictionary.Add(item.TytulKsiazki, item.Count);
                        }

                    }
                }
                foreach (var element in dictionary)
                {
                    MessageBox.Show(element.Key + element.Key.ToString());
                }

                foreach (var item in c.GroupBy(x => new { x.tytulKsiazki }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() }))
                {

                    bookList.Add($"Title: \"{ item.tytulKsiazki}\" \nWe have: {item.MyCount} examples\n");

                }
                booksList.ItemsSource = bookList;


            } }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

           
        }

        private void zwrot(object sender, RoutedEventArgs e)
        {
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();
            this.Close();
        }
        //Przycisk zamknij, zamyka okno
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Wybiera książki wg autora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void checkByGenre_Click(object sender, RoutedEventArgs e)
        {
            checkByGenreBook();
        }

        private void checkByGenreBook()
        {
          


            List<string> l = new List<string>();
            List<int> idKsiazek = new List<int>();
            if (Genres.SelectedItem == null)
            {
                MessageBox.Show("Musisz wybrać element!");
            }
            else { wynik = Genres.SelectedItem.ToString(); }
            var genre = wynik;

            string Gatunek = "";
            int idKsiazki = 1;
            var GenreIdQuery = context.gatunki.Where(x => x.gatunek == genre).Select(x => x.gatunek);
            var query2 = context.gatunki.SelectMany(x => x.Ksiazki, (x, ksiazki) => new { x.gatunek, ksiazki = ksiazki.idKsiazki }).Where(x => x.gatunek == GenreIdQuery.FirstOrDefault()).ToList();
            var query1 = context.gatunki;
            var query = from k in context.Ksiazki select k.gatunki;
            string s = "";
            foreach (var item in query)
            {
                Console.WriteLine(item);
                s += item.ToString();
            }

            foreach (var item in query2)
            {

                Gatunek = item.gatunek;
                idKsiazki = item.ksiazki;
                idKsiazek.Add(idKsiazki);

            }
            var ksiazkiGatunki = context.Ksiazki.Where(t => idKsiazek.Contains(t.idKsiazki)).Select(x => new { x.tytulKsiazki, x.idKsiazki }).ToList();

            foreach (var item in ksiazkiGatunki)
            {
                foreach (var i in context.Egzemplarze.Where(x => x.idKsiazki == item.idKsiazki).GroupBy(x => new { item.tytulKsiazki }).Select(g => new { g.Key.tytulKsiazki, MyCount = g.Count() }))
                {
                    l.Add($"Title: \"{ item.tytulKsiazki}\" \nWe have: {i.MyCount} examples\n");
                }
            }

            booksList.ItemsSource = l;


        }

        private void BorrowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            BorrowBook dashboard = new BorrowBook();
            dashboard.Show();
        }

        private void UserButton_Checked(object sender, RoutedEventArgs e)
        {

            ShowContextMenu();

            // contentMenu.IsVisible=true;


        }

        private void ShowContextMenu()
        {
            var contextMenu = Resources["contentMenu"] as ContextMenu;
            contentMenu.IsOpen = true;

        }

        private void ContentMenuClick(object sender, RoutedEventArgs e)
        {

            var item = e.OriginalSource as MenuItem;
            if (item.Header.ToString() == "Log Out")
            {
                this.Close();
                LoginScreen login = new LoginScreen();
                login.Show();
            }
            if (item.Header.ToString() == "New User")
            {
                AddUser addUser = new AddUser();

                addUser.Show();

            }
        }
    }

}
