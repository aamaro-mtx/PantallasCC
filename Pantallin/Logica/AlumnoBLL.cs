using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Datos;

namespace Negocio
{
    public class AlumnoBLL
    {
        AlumnosDAL _alumno;

        public AlumnoBLL()
        {
            _alumno = new AlumnosDAL();           
        }

        public List<SesionActual> ObtenerTodos()
        {
            return _alumno.ObtenerTodos();
        }

        public List<sActiva> GetByLab(int idLab)
        {
            return _alumno.GetByLab(idLab);
        }

        public List<sActiva> getActivas(int id)
        {
            return _alumno.GetActivas()
                .Where(a => a.SE_NUM_LAB == (id))
                .ToList();
        }
    }
}
