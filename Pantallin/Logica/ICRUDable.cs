using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
    interface ICRUDable<T> 
    {
        decimal Agregar(T entidad);
        bool Modificar(T entidad);
        bool Eliminar(T entidad);
        List<T> ObtenerAll();
        T ObtenerById(int id);
    }
}
