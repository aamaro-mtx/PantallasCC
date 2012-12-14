using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class LaboratorioDAL : ICRUDable<Laboratorio>
    {
        ContextoPantallas context;

        public LaboratorioDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Laboratorio Agregar(Laboratorio entidad)
        {
            try
            {
                context.Laboratorios.AddObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Laboratorio entidad)
        {
            try
            {
                context.Laboratorios.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Laboratorio entidad)
        {
            try
            {
                context.Laboratorios.Attach(entidad);
                context.Laboratorios.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Laboratorio> ObtenerAll()
        {
            try
            {
                return context.Laboratorios.ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Laboratorio ObtenerById(int id)
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
