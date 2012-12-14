using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class AvisoDAL : ICRUDable<Aviso>
    {
        ContextoPantallas context;
        
        public AvisoDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Aviso Agregar(Aviso entidad)
        {
            try
            {
                context.Avisos.AddObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Aviso entidad)
        {
            try
            {
                context.Avisos.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Aviso entidad)
        {
            try
            {
                context.Avisos.Attach(entidad);
                context.Avisos.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Aviso> ObtenerAll()
        {
            try
            {
                return
                context.Avisos.ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Aviso ObtenerById(int id)
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
