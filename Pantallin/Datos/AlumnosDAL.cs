using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class AlumnosDAL
    {
        AlumnosEntities context;

        public AlumnosDAL()
        {
            context = new AlumnosEntities();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public List<SesionActual> ObtenerTodos()
        {

            try { return context.SesActuales.Include("Sesion").Include("Usuario").ToList(); }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<sActiva> GetByLab(int idLab)
        {
            try
            {
                //return
                //this.ObtenerTodos().Where(t => t.Sesion.SE_NUM_LAB == idLab).ToList();
                var regreso = context.sActivas.ToList()
                   .Where(s => s.SE_NUM_LAB == idLab)
                   .ToList();
              return regreso;
            }
            catch (Exception ex)
            {
                throw (ex);
            }    
        }

        public List<sActiva> GetActivas()
        {
            try
            {
                return   context.sActivas.ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
