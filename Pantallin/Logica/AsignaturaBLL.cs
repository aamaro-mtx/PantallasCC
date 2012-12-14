using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Datos;

namespace Negocio
{
    public class AsignaturaBLL : ICRUDable<Asignatura>
    {
        AsignaturaDAL _asignatura;

        public AsignaturaBLL()
        {
            _asignatura = new AsignaturaDAL();
        }
        
        public decimal Agregar(Asignatura entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _asignatura.Agregar(entidad);
                if (r != null)
                {
                    resultado = r.ID;
                }
            }
            catch (Exception)
            {
                resultado = -1;
            }
            return resultado;
        }

        public bool Modificar(Asignatura entidad)
        {
            bool Resultado = false;
            try
            {
                _asignatura.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Asignatura entidad)
        {
            bool Resultado = false;
            try
            {
                _asignatura.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        public List<Asignatura> ObtenerAll()
        {
            List<Asignatura> Resultado = null;
            try
            {
                Resultado = _asignatura.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Asignatura ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}