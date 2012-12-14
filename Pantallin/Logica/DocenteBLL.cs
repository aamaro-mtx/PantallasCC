using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;

namespace Negocio
{
    public class DocenteBLL : ICRUDable<Docente>
    {
        DocenteDAL _docente;
        public DocenteBLL()
        {
            _docente = new DocenteDAL();
        }

        public decimal Agregar(Docente entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _docente.Agregar(entidad);
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

        public bool Modificar(Docente entidad)
        {
            bool Resultado = false;
            try
            {
                _docente.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Docente entidad)
        {
            bool Resultado = false;
            try
            {
                _docente.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        public List<Docente> ObtenerAll()
        {
            List<Docente> Resultado = null;
            try
            {
                Resultado = _docente.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Docente ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
