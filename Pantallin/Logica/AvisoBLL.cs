using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;

namespace Negocio
{
    public class AvisoBLL : ICRUDable<Aviso>
    {
        AvisoDAL _aviso;

        public AvisoBLL()
        {
            _aviso = new AvisoDAL();
        }

        public decimal Agregar(Aviso entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _aviso.Agregar(entidad);
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

        public bool Modificar(Aviso entidad)
        {
            bool Resultado = false;
            try
            {
                _aviso.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Aviso entidad)
        {
            bool Resultado = false;
            try
            {
                _aviso.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        public List<Aviso> ObtenerAll()
        {
            List<Aviso> Resultado = null;
            try
            {
                Resultado = _aviso.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Aviso ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
