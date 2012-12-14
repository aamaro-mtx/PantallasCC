using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Datos;

namespace Negocio
{
    public static class CentroComputoBLL
    {
        public static laboratorio ObtenerLab(int id)
        {
            CentroComputo c = CentroDAL.LoadFromXML();
            return c.Laboratorios.Where(l => l.Numero == id).FirstOrDefault();
        }

        public static CentroComputo LoadFromXML()
        {
            return CentroDAL.LoadFromXML();
        }
    }
}
