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
    /// Interaction logic for ExampleAdd.xaml
    /// </summary>
    public partial class ExampleAdd : Window
    {
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();
        public ExampleAdd()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            int idKsiazki = Convert.ToInt32(bookId.Text);
           
            foreach (var item in context.Ksiazki.Select(x => x.idKsiazki))
            {
                if (item != idKsiazki)
                {
                    MessageBox.Show("Book with same id doesnt exists");
                }
                else
                {
                    Egzemplarze egzemplarz = new Egzemplarze()
                    {
                        idEgzemplarza = Convert.ToInt32(exampleId),
                        idKsiazki = idKsiazki,

                    };

                    var query = context.Egzemplarze.Select(x => x.idEgzemplarza).FirstOrDefault();
                    if (Convert.ToInt32(exampleId) == query)
                    {
                        MessageBox.Show("Same id already exists");
                    }
                    else
                    {
                        context.Egzemplarze.Add(egzemplarz);
                        context.SaveChanges();
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
