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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektBiblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var dataSet = new libraryEntitiesDataSet();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CheckBooks dashboard = new CheckBooks();
            dashboard.Show();
        }

        private void zwrot(object sender, RoutedEventArgs e)
        {
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Management dashboard = new Management();
            dashboard.Show();
           
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            BorrowBook dashboard = new BorrowBook();
            dashboard.Show();
           
        }

       

        private void UserButton_Checked(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
           
        }

        private void Raport_Click(object sender, RoutedEventArgs e)
        {
            Excel excel = new Excel(Excel.ReportType.TheMostPopularBook);
        }
    }
}
