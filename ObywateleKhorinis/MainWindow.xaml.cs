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
using System.Xml.Serialization;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ObywateleKhorinis
{

    public partial class MainWindow : Window
    {
        private List<Obywatel> m_oObywatelList = null;


       // private string numerId;
        public MainWindow()
        {
            //string id = text;
            //numerId = text;
            InitializeComponent();
            InitBinding();
        }
        

        private void InitBinding()
        {
            m_oObywatelList = new List<Obywatel>();
            try
            {
                using (var reader = new StreamReader("ListaObywateli.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Obywatel>),
                        new XmlRootAttribute("ArrayOfObywatel"));
                    m_oObywatelList = (List<Obywatel>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }
            if (m_oObywatelList.Count == 0)
            {
                m_oObywatelList.Add(new Obywatel(1, "Vatras", "Dolne miasto", "Adanos", "C:\\Users\\micha\\OneDrive\\Pulpit\\Lista 4\\Vatras.png"));
                m_oObywatelList.Add(new Obywatel(2, "Larius", "Górne miasto", "Beliar", "C:\\Users\\micha\\OneDrive\\Pulpit\\Lista 4\\Larius.png"));
                m_oObywatelList.Add(new Obywatel(3, "Thorben", "Dolne miasto", "Innos", "C:\\Users\\micha\\OneDrive\\Pulpit\\Lista 4\\Thorben.png"));
            }
            listaObywateli.ItemsSource = m_oObywatelList;

        }

        private void _this_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new XmlSerializer(m_oObywatelList.GetType());
            using (var writer = XmlWriter.Create("ListaObywateli.xml"))
            {
                serializer.Serialize(writer, m_oObywatelList);
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Window1 dodaj = new Window1();
            dodaj.Show();
        }

        public void DanePlik_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var reader = new StreamReader("ListaObywateli.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Obywatel>),
                        new XmlRootAttribute("ArrayOfObywatel"));
                    m_oObywatelList = (List<Obywatel>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }
            listaObywateli.ItemsSource = m_oObywatelList;
        }

        private void Edycja(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2(SzukaneId.Text);
            win2.Show();
        }
       

        private void Baza(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-5C3HNUUU\SQLEXPRESS;Database=projekt;User ID=projekt; Password=projekt ;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Połączenie Nawiązane");
            //cnn.Close();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select * From Obywatele";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "\n";
            }
            MessageBox.Show(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            m_oObywatelList.RemoveAt(Convert.ToInt32(SzukaneId.Text) - 1);
            var serializer = new XmlSerializer(m_oObywatelList.GetType());
            using (var writer = XmlWriter.Create("ListaObywateli.xml"))
            {
                serializer.Serialize(writer, m_oObywatelList);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            win3.Show();
        }
    }
}
