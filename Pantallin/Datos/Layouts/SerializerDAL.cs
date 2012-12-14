using Entidades;
using System.IO;
using System.Xml.Serialization;

namespace Datos
{
    public static class SerializerDAL
    {
        public static CentroComputo LoadFromXML()
        {
            CentroComputo centro = new CentroComputo();
            XmlSerializer Almacen = new XmlSerializer(typeof(CentroComputo));
            FileStream Archivo = File.Open("Layout.xml", FileMode.Open, FileAccess.Read);
            centro = (CentroComputo)Almacen.Deserialize(Archivo);
            Archivo.Close();
            return centro;
        }
    }
}
