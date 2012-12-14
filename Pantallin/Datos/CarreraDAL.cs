using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Datos
{
    public class CarreraDAL : ICRUDable<Carrera>
    {
        ContextoPantallas context;

        public CarreraDAL()
        {
            context = new ContextoPantallas();
            context.ContextOptions.ProxyCreationEnabled = false;
        }

        public Carrera Agregar(Carrera entidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Carrera entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Carrera entidad)
        {
            throw new NotImplementedException();
        }

        public List<Carrera> ObtenerAll()
        {
            try
            {
                return
                context.Carreras.ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Carrera ObtenerById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
