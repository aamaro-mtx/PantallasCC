using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    interface ICRUDable<T> :IDisposable where T:class
    {
        T Agregar(T entidad);
        void Modificar(T entidad);
        void Eliminar(T entidad);
        List<T> ObtenerAll();
        T ObtenerById(int id);
    }
}
