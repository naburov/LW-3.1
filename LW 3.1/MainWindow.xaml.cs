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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace LW_3._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //static BindingSource bndSource = new BindingSource();
        static MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = txtSQLInput.Text;
                if (sql.Length == 0) throw new Exception();
                DataTable dt = CreateDB(sql);
                dgridB.ItemsSource = dt.DefaultView;
            }catch(Exception k)
            {
                txtConsole.Text += "\r\n"+k.Message;
            }
            ////var myData = dt.Select();
            //string[] colNames = new string[dt.Columns.Count];
            //for (int i = 0; i < dt.Columns.Count; i++)
            //    colNames[i] = dt.Columns[i].ColumnName;

            //foreach (string name in colNames)
            //    dgridB.Columns.Add

            //foreach (DataRow row in dt.Rows)
            //{
            //    var cells = row.ItemArray;
            //    dgridB.Items.Add(cells);

            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtConsole.Text = "Getting Connection ...\r\n";
            connection = DBUtils.GetDBConnection();
            try
            {
                txtConsole.Text += ("Openning Connection ...\r\n");

                connection.Open();

                txtConsole.Text += ("Connection successful!");
            }
            catch (Exception k)
            {
                txtConsole.Text = ("Error" + k.Message);
            }
        }

        private static DataTable CreateDB(string sql)
        {
            connection = DBUtils.GetDBConnection();
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            connection.Open();
            //sqlCom.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }

        private void dgridB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
