using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Entidades;

namespace Negocio
{
    public class LaboratorioBLL : ICRUDable<Laboratorio>
    {
        LaboratorioDAL _laboratorio;
        public LaboratorioBLL()
        {
            _laboratorio = new LaboratorioDAL();
        }

        public decimal Agregar(Laboratorio entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _laboratorio.Agregar(entidad);
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

        public bool Modificar(Laboratorio entidad)
        {
            bool Resultado = false;
            try
            {
                _laboratorio.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Laboratorio entidad)
        {
            bool Resultado = false;
            try
            {
                _laboratorio.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        public List<Laboratorio> ObtenerAll()
        {
            List<Laboratorio> Resultado = null;
            try
            {
                Resultado = _laboratorio.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Laboratorio ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
