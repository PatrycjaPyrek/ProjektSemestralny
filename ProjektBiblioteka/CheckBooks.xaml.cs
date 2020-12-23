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
        public CheckBooks()
        {
            InitializeComponent();
            //libraryEntitiesDataSet context = new libraryEntitiesDataSet();
            libraryEntitiesDataSet context = new libraryEntitiesDataSet();
            List<string> bookList = new List<string>();
            var c = from e in context.Ksiazki join ek in context.Egzemplarze on e.idKsiazki equals ek.idKsiazki group ek.Ksiazki by e.tytulKsiazki into newGroup orderby newGroup.Key select new { newGroup } ;
            foreach (var item in c)
            {
                
                bookList.Add(item.newGroup.Key);
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
    }
    
}
