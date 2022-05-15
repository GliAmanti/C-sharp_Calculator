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
using static hw_calculator.MySqlClass;

namespace hw_calculator
{
    /// <summary>
    /// DBtable.xaml 的互動邏輯
    /// </summary>
    public partial class DBtable : Window
    {
        MySqlClass mySql = new MySqlClass();
        string query_select = "select * from record";

        public DBtable()
        {
            InitializeComponent();

            List<CustomRecord> record = MySqlClass.DBConnection2(query_select);
            myList.ItemsSource = record;
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            CustomRecord select = (CustomRecord)myList.SelectedItem;
            string query_delete = "delete from record where record.expression = '" + select.Expression + "'";
            mySql.DBDelete(query_delete);

            List<CustomRecord> record = MySqlClass.DBConnection2(query_select);
            myList.ItemsSource = record;
        }

        private void btn_previous_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
