using Entidades;
using System.IO;
using System.Xml.Serialization;

namespace Datos
{
    public class CentroDAL
    {
        public static void SaveToXML(CentroComputo centro)
        {
            XmlSerializer Almacen = new XmlSerializer(typeof(CentroComputo));

            FileStream archivo = File.Create("Layout.xml");
            Almacen.Serialize(archivo, centro);
            archivo.Close();
        }

        public static void SaveToBinary(CentroComputo centro)
        {
            XmlSerializer Almacen = new XmlSerializer(typeof(CentroComputo));
        }

        public static CentroComputo LoadFromXML()
        {                                   
            CentroComputo centro = new CentroComputo();
            return (CentroComputo)SerializerDAL.LoadFromXML();
        }
    }
}
