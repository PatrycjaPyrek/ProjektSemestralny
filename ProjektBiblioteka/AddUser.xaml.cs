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
    public partial class AddUser : Window
    {
        public AddUser()
        {
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
            foreach (var item in context.loginPass.Select(x=>x.username))
            {
                if (item.ToLower() == usernametxt.Text.ToLower())
                {
                    isFree = false;
                }
            }
            if (isFree == true)
            {
                context.loginPass.Add(account);
                context.SaveChanges();
                MessageBox.Show($"New user was added. Login:{usernametxt.Text},password: {passwordtxt.Text}");
            }
            else
            {
                MessageBox.Show("User with the same username already exists. Please try different name.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}