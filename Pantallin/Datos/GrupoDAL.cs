using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class GrupoDAL:ICRUDable<Grupo>
    {
        ContextoPantallas context;

        public GrupoDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Grupo Agregar(Grupo entidad)
        {
            try
            {
                context.Grupos.AddObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Grupo entidad)
        {
            try
            {
                context.Grupos.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Grupo entidad)
        {
            try
            {
                context.Grupos.Attach(entidad);
                context.Grupos.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Grupo> ObtenerAll()
        {
            try
            {
                return context.Grupos.ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Grupo ObtenerById(int id)
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
