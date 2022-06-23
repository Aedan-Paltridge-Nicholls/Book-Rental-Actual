using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Book_Rental_Actual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
       
        public Login()
        {
            InitializeComponent();
       
        }
           databasemanager objDb = new databasemanager();
        private void Button1_Click_1(object sender, RoutedEventArgs e)
        {
            if (objDb.Login(UsernameBox.Text, PasswordBox.Text))
            {
                MessageBox.Show("Login Successful");
                MainWindow w = new MainWindow();
                this.Visibility = System.Windows.Visibility.Hidden;
                w.ShowDialog();
                this.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }

    class databasemanager
    {
        SqlConnection SqlConn = new SqlConnection
            (@"Data Source=DESKTOP-P39M3QI\SQLEXPRESS;Initial Catalog=BookRental;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False ");
        SqlCommand SqlSrt = new SqlCommand();
        SqlDataReader SqlReader;
        string sqlStmt;

        public bool Login(string username, string Password)
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "Select * from login where Username='" + username
                   + "' and Password='" + Password + "'";
                SqlSrt.CommandText = sqlStmt;
                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();
                
                if (SqlReader.HasRows) { SqlConn.Close(); return true; }
                else { SqlConn.Close(); return false; }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
                return false;
            }

        }
    }
}
