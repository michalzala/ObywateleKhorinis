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
using System.Xml.Serialization;

namespace ObywateleKhorinis
{
    public class Obywatel
    {
        [XmlAttribute("id")]
        public int ObywatelId { get; set; }
        [XmlElement("Imie")]
        public string Imie { get; set; }
        [XmlElement("Hierarchia")]
        public string Hierarchia { get; set; }
        [XmlElement("Wyznanie")]
        public string Wyznanie { get; set; }
        [XmlElement("Obraz")]

        public string Obraz { get; set; }



        public Obywatel(int nObywatelId, string nImie, string nHierarchia, string nWyznanie, string nObraz)
        {
            ObywatelId = nObywatelId;
            Imie = nImie;
            Hierarchia = nHierarchia;
            Wyznanie = nWyznanie;
            Obraz = nObraz;
        }

        public Obywatel()
        {
            ObywatelId = 1;
            Imie = "Lord Hagen";
            Hierarchia = "Górne miasto";
            Wyznanie = "Innos";
            Obraz = "";
        }
    }
}
