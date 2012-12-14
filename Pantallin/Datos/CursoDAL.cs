using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class CursoDAL:ICRUDable<Curso>
    {
        ContextoPantallas context;

        public CursoDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Curso Agregar(Curso entidad)
        {
            try
            {
                context.Cursos.AddObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return entidad;
        }

        public void Modificar(Curso entidad)
        {
            try
            {
                context.Cursos.Attach(entidad);
                context.ObjectStateManager.ChangeObjectState(entidad, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public void Eliminar(Curso entidad)
        {
            try
            {
                context.Cursos.Attach(entidad);
                context.Cursos.DeleteObject(entidad);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Curso> ObtenerAll()
        {
            try
            {
                return context.Cursos.ToList();
            }
            catch (Exception ex)
            {
                
                throw (ex);
            }
        }

        public Curso ObtenerById(int id)
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
