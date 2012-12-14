using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;

namespace Negocio
{
    public class GrupoBLL : ICRUDable<Grupo>
    {
        GrupoDAL _grupo;
        public GrupoBLL()
        {
            _grupo = new GrupoDAL();
        }

        public decimal Agregar(Grupo entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _grupo.Agregar(entidad);
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

        public bool Modificar(Grupo entidad)
        {
            bool Resultado = false;
            try
            {
                _grupo.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Grupo entidad)
        {
            bool Resultado = false;
            try
            {
                _grupo.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        public List<Grupo> ObtenerAll()
        {
            List<Grupo> Resultado = null;
            try
            {
                Resultado = _grupo.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Grupo ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
