using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class DocenteDAL:ICRUDable<Docente>
    {
        ContextoPantallas context;
        public DocenteDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Docente Agregar(Docente entidad)
        {
            try
            {
                context.Docentes.AddObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Docente entidad)
        {
            try
            {
                context.Docentes.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Docente entidad)
        {
            try
            {
                context.Docentes.Attach(entidad);
                context.Docentes.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Docente> ObtenerAll()
        {
            try
            {
                return context.Docentes.ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Docente ObtenerById(int id)
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
