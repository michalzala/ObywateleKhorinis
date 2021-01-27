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
using System.Xml.Serialization;


namespace ObywateleKhorinis

{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string gdzie;
        private List<Obywatel> m_oObywatelList = null;

        public Window1()
        {
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

            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage(new Uri("C:\\Users\\micha\\OneDrive\\Pulpit\\Lista 4\\Vatras.png"));
            Pokaz_obraz.Source = bitmapImage;
        }

        private void DodajObywatela_Click(object sender, RoutedEventArgs e)
        {


            var serializer = new XmlSerializer(m_oObywatelList.GetType());
            m_oObywatelList.Add(new Obywatel(m_oObywatelList.Count + 1, Imie.Text, Hierarchia.Text, Wyznanie.Text, gdzie));
            using (var writer = XmlWriter.Create("ListaObywateli.xml"))
            {
                serializer.Serialize(writer, m_oObywatelList);
            }
            MessageBox.Show("Dodano");
        }

        public void WstawObraz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.png; *.bmp;)|*.jpg; *.jpeg; *.png; *.gif; *.bmp;";

            if (openFileDialog.ShowDialog() == true)
            {
                gdzie = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                Pokaz_obraz.Source = new BitmapImage(fileUri);
            }

        }


        
    }
}
