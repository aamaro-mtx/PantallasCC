using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;

namespace Negocio
{
    public class CarreraBLL : ICRUDable<Carrera>
    {
        CarreraDAL _carreras;
        public CarreraBLL()
        {
            _carreras = new CarreraDAL();
        }

        public decimal Agregar(Carrera entidad)
        {
            throw new NotImplementedException();
        }

        public bool Modificar(Carrera entidad)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Carrera entidad)
        {
            throw new NotImplementedException();
        }

        public List<Carrera> ObtenerAll()
        {
            List<Carrera> Resultado = null;
            try
            {
                Resultado = _carreras.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Carrera ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
