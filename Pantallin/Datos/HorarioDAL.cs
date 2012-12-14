using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class HorarioDAL : ICRUDable<Horario>
    {
        ContextoPantallas context;

        public HorarioDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Horario Agregar(Horario entidad)
        {
            try
            {
                context.Horarios.AddObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Horario entidad)
        {
            try
            {
                context.Horarios.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Horario entidad)
        {
            try
            {
                context.Horarios.Attach(entidad);
                context.Horarios.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Horario> ObtenerAll()
        {
            try
            {
                return context.Horarios.Include("Docente").Include("Laboratorio").Include("Asignatura").Include("Grupo").ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Horario ObtenerById(int id)
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
