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
        public DataTable Listbook(string Bookname)
        {
            DataTable DT = new DataTable();
            try 
            {
            
            }
        }

    }
}
