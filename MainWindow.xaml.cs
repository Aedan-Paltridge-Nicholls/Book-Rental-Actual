using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Book_Rental_Actual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        public DataTable ListBooks(string Bookname)
        {
            DataTable DT = new DataTable();
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "Select * from Books where BookName like '"+Bookname+"'";
                SqlSrt.CommandText=sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();
                if(SqlReader.HasRows) { DT.Load(SqlReader); }
               SqlConn.Close();
                return DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();               
                return null;
            }
        }
        public DataTable ListCustomers(string Custname)
        {
            DataTable DT = new DataTable();
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "Select * from Users where FullName like '" + Custname + "'";
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();
                if (SqlReader.HasRows) { DT.Load(SqlReader); }
                SqlConn.Close();
                return DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
                return null;
            }
        }
        public void Addbooks(string bookname,string author) 
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "insert into Books(Bookname,Author,Available) values('"+ bookname+"','"+author+"','Yes')";
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
            }
        }
        public void Deletebooks(string bookname)
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "delete from Books where Bookname like '"+bookname+"'" ;
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
            }
        }
        public void AddCustomer(string userName, string Fullname,string Age )
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "insert into Users(Username,Age,Fullname) " +
                    "values('" + userName + "','" +Age + "','" + Fullname + "')";
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
            }
        }
        public void DeleteCustomer(string Fullname)
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "delete from Users where Fullname like '" + Fullname + "'";
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
            }
        }
        public void IsusseBook(string CustomerName, string BookName )
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "Update Books set Borrower '" + CustomerName + "'" +"Available = 'NO' where BookName ='"+ BookName + "'" ;
                   
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
            }
        }
        public void ReturnBook( string BookName)
        {
            try
            {
                SqlSrt.Connection = SqlConn;
                sqlStmt = "Update Books set Borrower 'NULL'" + "Available = 'Yes' where BookName ='" + BookName + "'";
                SqlSrt.CommandText = sqlStmt;

                SqlConn.Open();
                SqlReader = SqlSrt.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception", ex.Message);
                SqlConn.Close();
            }
        }
       
    }
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

    
}
