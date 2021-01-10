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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        public DeleteUser()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool isFree = true;
            var context = new LoginBaseEntities();
            var account = new loginPass()
            {
                username = usernametxt.Text,
                password = passwordtxt.Text
            };
            var query = context.loginPass.Where(x => x.username == usernametxt.Text).Select(x => x).FirstOrDefault();
            foreach (var item in context.loginPass)
            {
                if (item.username == account.username && item.password==account.password)
                {
                    context.loginPass.Remove(query);

                    MessageBox.Show("User deleted.");
                    break;
                }
                MessageBox.Show("Incorrect login or password. Try again");
            }
            context.SaveChanges();




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}