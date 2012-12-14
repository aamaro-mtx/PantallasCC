using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public partial class Horario : IDisposable
    {

        //public void IDisposable.Dispose()
        //{
        //    this.Docente = null;
        //    this.Asignatura = null;
        //    this.Dia = null;
        //}
        public void Dispose()
        {
            this.Docente = null;
            this.Asignatura = null;
            this.Dia = null;
        }
    }
}
