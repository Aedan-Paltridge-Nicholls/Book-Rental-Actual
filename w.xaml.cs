using System;
using System.Collections.Generic;
using System.Data;
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
            if (dialogResult.ToString() == "yes") 
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
                obj.Deletebooks(Fullname);
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
        }
    }
}
