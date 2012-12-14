using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PUsuarios
{
    public static class Utilidades
    {

        public static bool Validar(TextBox t1, TextBox t2 = null)
        {
            bool estado = true;
            if (t2 != null)
                estado = !(string.IsNullOrEmpty(t1.Text) || string.IsNullOrEmpty(t2.Text)) ? true : false;
            else
                estado = !string.IsNullOrEmpty(t1.Text) ? true : false;
            return estado;
        }

        public static void Limpiar(TextBox t1, TextBox t2 = null, TextBox t3 = null)
        {
            t1.Clear();
            if (t2 != null)
                t2.Clear();
            if (t3 != null)
                t3.Clear();
            t1.Focus();            
        }
    }
}