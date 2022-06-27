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

namespace Book_Rental_Actual
{
    /// <summary>
    /// Interaction logic for w.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        databasemanager obj = new databasemanager();
        Stats stats = new Stats();
        public MainWindow()
        {
            InitializeComponent();
        }
     

        private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
             ListBooks.ItemsSource = obj.ListBooks("%").DefaultView;
             ListCustomers.ItemsSource = obj.ListCustomers("%").DefaultView;
        }

        private void Removebook_Click(object sender, RoutedEventArgs e)
        {

           MessageBoxResult dialogResult = MessageBox.Show("Are you sure you Want to delete"
                + Bookname.Text + " By " + Author.Text, "Library", MessageBoxButton.YesNo);
            if (dialogResult.ToString() == "Yes") 
            { 
             DataRowView Row = (DataRowView)ListBooks.SelectedItems[0];
                String bookname = Convert.ToString(Row["BookName"]);
                obj.Deletebooks(bookname);
                MessageBox.Show("Book Deleted");
                ListBooks.ItemsSource = obj.ListBooks("%").DefaultView;
            
            }
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
          
            if (Bookname.Text != "" && Author.Text != "")
            {
                obj.Addbooks(Bookname.Text, Author.Text);
            }
            ListBooks.ItemsSource = obj.ListBooks("%").DefaultView;
        }

        private void RemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you Want to delete"
                +"User"+ Username.Text+"full name" + 
                Fullname.Text +" age " +Age.Text, "Library", MessageBoxButton.YesNo);
            if (dialogResult.ToString() == "yes")
            {
                DataRowView Row = (DataRowView)ListCustomers.SelectedItems[0];
                String Fullname = Convert.ToString(Row["Fullname"]);
                obj.DeleteCustomer(Fullname);
                MessageBox.Show("User Deleted");
                ListCustomers.ItemsSource = obj.ListBooks("%").DefaultView;

            }
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text != "" && Age.Text != ""&& Fullname.Text != "" )
            {
                obj.AddCustomer(Username.Text, Fullname.Text, Age.Text);
            }
            ListCustomers.ItemsSource = obj.ListBooks("%").DefaultView;
        }

        private void IsusseBook_Click(object sender, RoutedEventArgs e)
        {
            if(ListBooks.SelectedIndex > 0 && ListCustomers.SelectedIndex > 0)
            {
                DataRowView BookRow = (DataRowView)ListBooks.SelectedItems[0];
                string Bookname = Convert.ToString(BookRow["Bookname"]);

                DataRowView CustomerRow = (DataRowView)ListCustomers.SelectedItems[0];
                string CustomerName = Convert.ToString(BookRow["Bookname"]);
                obj.IsusseBook(CustomerName, Bookname);
                ListBooks.ItemsSource = obj.ListBooks("%").DefaultView;
                MessageBox.Show("Book Isussed");

            }
            else { MessageBox.Show("Please select a customer and Book to isusse"); }
        }

        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (ListBooks.SelectedIndex > 0) 
            {
                DataRowView BookRow = (DataRowView)ListBooks.SelectedItems[0];
                string Bookname = Convert.ToString(BookRow["Bookname"]);

                obj.ReturnBook(Bookname);
                ListBooks.ItemsSource = obj.ListBooks("%").DefaultView;
                MessageBox.Show("Book Returned");



            }
            else { MessageBox.Show("Please select a Book to Return"); }

        }

        public class Stats
        {
            SqlConnection SqlConn = new SqlConnection
            (@"Data Source=DESKTOP-P39M3QI\SQLEXPRESS;Initial Catalog=BookRental;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False ");
            SqlCommand SqlSrt = new SqlCommand();
            SqlDataReader SqlReader;
            string sqlStmt;
            public int BookNum()
            {
                int BookNUM = 0;
                try
                {
                    SqlSrt.Connection = SqlConn;
                    sqlStmt = "Select * from Books ";
                    SqlSrt.CommandText = sqlStmt;

                    SqlConn.Open();
                    SqlReader = SqlSrt.ExecuteReader();
                    while (SqlReader.Read())
                    { 
                        BookNUM++;                  
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Exception BookNUM", ex.Message);
                

                }
                SqlConn.Close();
                return BookNUM;
            }
            public int UserNUMBER()
            {
                int UserNUM = 0;
                try
                {
                    SqlSrt.Connection = SqlConn;
                    sqlStmt = "Select * from Users ";
                    SqlSrt.CommandText = sqlStmt;

                    SqlConn.Open();
                    SqlReader = SqlSrt.ExecuteReader();
                    while (SqlReader.Read())
                    {
                        UserNUM++;

                    }
                    SqlConn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Exception UserNUM", ex.Message);
                    SqlConn.Close();

                }

                return UserNUM;

            }
            public int IssuedBookNUMBER()
            {
                int IssuedNUM = 0;
                try
                {
                    SqlSrt.Connection = SqlConn;
                    sqlStmt = "Select Available from Books ";
                    SqlSrt.CommandText = sqlStmt;

                    SqlConn.Open();
                    SqlReader = SqlSrt.ExecuteReader();
                    while (SqlReader.Read())
                    {
                        if (SqlReader.HasRows)
                        {
                            if (Convert.ToString(SqlReader) == "NO")
                            {
                                IssuedNUM++;
                                
                            }
                        }
                    }
                    
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Exception IsusseNUM", ex.Message);
                

                }
                SqlConn.Close();
                return IssuedNUM;

            }
        }
           
        private void Stats_Click(object sender, RoutedEventArgs e)
        {
           
            int NumIssuedBooks = stats.IssuedBookNUMBER();
            int NumBook = stats.BookNum();
            int NumUsers = stats.UserNUMBER();

            MessageBox.Show("Total number of Books :"+NumBook
                +"\n Total number of Users :"+ NumUsers 
                + "\n Total number of Issued Books :"+ NumIssuedBooks,
                "Library Stats",MessageBoxButton.OK);


        }
    }

}
