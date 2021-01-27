using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xaml;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;

namespace ObywateleKhorinis
{
    /// <summary>
    /// Logika interakcji dla klasy Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void DodajObywatela_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            string connectionString = @"Data Source=LAPTOP-5C3HNUUU\SQLEXPRESS;Database=projekt;User ID=projekt; Password=projekt ;";

            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "insert into Obywatele(Imie, Hierarchia, Wyznanie) values(@Imie,@Hierarchia, @Wyznanie)";

            cmd.Parameters.AddWithValue("@Imie", Imie.Text);
            cmd.Parameters.AddWithValue("@Hierarchia", Hierarchia.Text);
            cmd.Parameters.AddWithValue("@Wyznanie", Wyznanie.Text);
           


            cmd.ExecuteNonQuery();
            MessageBox.Show("Success");
        }

        private void WstawObraz_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
