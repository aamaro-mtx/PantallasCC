using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Datos;

namespace Negocio
{
    public class CursoBLL : ICRUDable<Curso>
    {
        CursoDAL _curso;
        public CursoBLL()
        {
            _curso = new CursoDAL();
        }
        public decimal Agregar(Curso entidad)
        {
            decimal resultado = -1;
            try
            {
                var r = _curso.Agregar(entidad);
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

        public bool Modificar(Curso entidad)
        {
            bool Resultado = false;
            try
            {
                _curso.Modificar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        public bool Eliminar(Curso entidad)
        {
            bool Resultado = false;
            try
            {
                _curso.Eliminar(entidad);
                Resultado = true;
            }
            catch (Exception)
            {
                Resultado = false;
            }
            return Resultado;
        }

        public List<Curso> ObtenerAll()
        {
            List<Curso> Resultado = null;
            try
            {
                Resultado = _curso.ObtenerAll();
            }
            catch (Exception)
            {
                Resultado = null;
            }
            return Resultado;
        }

        public Curso ObtenerById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
