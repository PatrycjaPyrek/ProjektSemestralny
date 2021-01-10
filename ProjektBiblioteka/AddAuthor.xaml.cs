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
    /// Interaction logic for AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : Window
    {
        libraryEntitiesDataSet context = new libraryEntitiesDataSet();
        public AddAuthor()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        bool available = true;
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
           
            int id = Convert.ToInt32(idAutora.Text);
            foreach (var item in context.Tworcy.Select(x=>x.idTworcy))
            {
                if (id == item)
                {
                    available = false;
                    MessageBox.Show("Author with same id already exists. Try again");
                }
                else
                {
                    available = true;
                }
            }
            int? year = null;
            if (notKnownYear.IsChecked==true)
            {
                year = null;
            }
            else
            {
                year = Convert.ToInt32(birthYear.Text);
            }
            if(notKnownYear.IsChecked==true && birthYear.Text != "")
            {
                MessageBox.Show("Please specify if a year is known.");
            }
            
            if (available == true)
            {
                Tworcy tworca = new Tworcy()
                {
                    idTworcy = id,
                    imieTworcy = firstName.Text,
                    nazwiskoTworcy = lastName.Text,
                    rokUrodzenia = year
                };
                context.Tworcy.Add(tworca);
                context.SaveChanges();
            }
          
                   
                

                
           
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
