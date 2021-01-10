using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            var context = new libraryEntitiesDataSet();

            // context2.loginPass.Find();
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        /// <summary>
        /// Przycisk LogIn umożliwiający logowaniu. Po poprawnym podaniu 
        /// loginu i hasła (odpowiadające rekordy w bazie danych) uruchamia się nowe okno aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            var context2 = new LoginBaseEntities();
            foreach (var item in context2.loginPass)
            {
                if (usernametxt.Text == item.username && item.password==passwordtxt.Password)
                {
                    
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();
                        this.Close();
                    
                }
            }
            //if (usernametxt.Text=="admin" && passwordtxt.Text=="admin")
            //{

            //    MainWindow dashboard = new MainWindow();
            //    dashboard.Show();
            //    this.Close();
            //}

            // SqlConnection sqlCon = new SqlConnection(@"Data Source=.\DESKTOP-TEBB0TS\SQLCOURSE2017;Initial Catalog=loginPass; Integrated Security = True;");
            // var result = from c in sqlCon.GetTable<>
            //try
            //{
            //    if (sqlCon.State == ConnectionState.Closed)
            //        sqlCon.Open();
            //    string query = "SELECT COUNT(*) FROM LoginPass where username=@username and password=@password";
            //    SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
            //    sqlCommand.CommandType = CommandType.Text;
            //    sqlCommand.Parameters.AddWithValue("@username", username.Text);
            //    sqlCommand.Parameters.AddWithValue("@password", password.Text);
            //    int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            //        if (count == 1)
            //        {
            //            MainWindow dashboard = new MainWindow();
            //            dashboard.Show();
            //            this.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show("login lub hasło jest niepoprawne");
            //        }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //}
            //finally
            //{
            //    sqlCon.Close();
            //}



        }
        /// <summary>
        /// Przycisk zamykający aplikację po naciśnięciu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void usernametxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
