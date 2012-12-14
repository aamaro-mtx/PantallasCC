using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class AsignaturaDAL : ICRUDable<Asignatura>
    {
        ContextoPantallas context;

        public AsignaturaDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public List<Asignatura> ObtenerAbsTodo()
        {
            try
            {
                return
                context.Asignaturas.Include("Curso").Include("Horario").ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public Asignatura Agregar(Asignatura entidad)
        {
            try
            {
                context.Asignaturas.AddObject(entidad);
                context.SaveChanges();                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Asignatura entidad)
        {
            try
            {
                context.Asignaturas.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Asignatura entidad)
        {
            try
            {
                context.Asignaturas.Attach(entidad);
                context.Asignaturas.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Asignatura> ObtenerAll()
        {
            try
            {
                return 
                context.Asignaturas.Include("Horario").Include("Curso").ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Asignatura ObtenerById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
