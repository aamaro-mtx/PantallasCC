using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;

namespace Negocio
{
    public class HorarioBLL : ICRUDable<Horario>
    {
        HorarioDAL _horario;
        public HorarioBLL()
        {
            _horario = new HorarioDAL();
        }

        public decimal Agregar(Horario entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _horario.Agregar(entidad);
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

        public bool Modificar(Horario entidad)
        {
            bool Resultado = false;
            try
            {
                _horario.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Horario entidad)
        {
            bool Resultado = false;
            try
            {
                _horario.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }
        public List<Horario> ObtenerByLab(int noLAb)
        {
            List<Horario> Resultado = null;
            try
            {
                Resultado = _horario.ObtenerAll().Where(h => h.IDLab == noLAb).OrderBy(t => t.HoraInicio).ToList();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public List<Horario> ObtenerAll()
        {
            List<Horario> Resultado = null;
            try
            {
                Resultado = _horario.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Horario ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
