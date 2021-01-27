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
  
    public partial class Window2 : Window
    {
        private List<Obywatel> m_oObywatelList = null;
        private string gdzie;
        private string numerId;

        public Window2(string text)
        {
            string id = text;
            numerId = text;
            InitializeComponent();
            InitBinding(id);
        }

        private void InitBinding(string id)
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
            Obywatel oFoundObywatel = m_oObywatelList.Find(oElement => oElement.ObywatelId == Convert.ToInt32(numerId));
            Imie.Text = oFoundObywatel.Imie;
            Hierarchia.Text = oFoundObywatel.Hierarchia;
            Wyznanie.Text = Convert.ToString(oFoundObywatel.Wyznanie);
            numerId = id;


        }

        private void Edycja_Click(object sender, RoutedEventArgs e)
        {
            ObywatelId.IsEnabled = false;
            Imie.IsEnabled = true;
            Hierarchia.IsEnabled = true;
            Wyznanie.IsEnabled = true;
            WstawO.IsEnabled = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Obywatel oFoundObywatel = m_oObywatelList.Find(oElement => oElement.ObywatelId == Convert.ToInt32(numerId));
            oFoundObywatel.Imie = Imie.Text;
            oFoundObywatel.Hierarchia = Hierarchia.Text;
            oFoundObywatel.Wyznanie = (Convert.ToString(oFoundObywatel.Wyznanie)); 
            oFoundObywatel.Obraz = gdzie;

            MessageBox.Show("Nowe dane zostały zapisane!");

            var serializer = new XmlSerializer(m_oObywatelList.GetType());
            using (var writer = XmlWriter.Create("ListaObywateli.xml"))
            {
                serializer.Serialize(writer, m_oObywatelList);
            }
        }

        private void WstawObraz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Filses(*.jpg; *.jpeg; *.gif; *.png; *.bmp;)|*.jpg; *.jpeg; *.png; *.gif; *.bmp;";

            if (openFileDialog.ShowDialog() == true)
            {
                gdzie = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                Pokaz_obraz.Source = new BitmapImage(fileUri);
            }


        }
    }
}
