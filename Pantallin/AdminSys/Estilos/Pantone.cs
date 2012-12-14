using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PUsuarios
{
    public static class Pantone
    {
        private  static Color Verde = Color.FromRgb(1,53,74);
        private static Color Naranja = Color.FromRgb(231, 126, 55);
        public static Brush bVerde = new SolidColorBrush(Verde);
        public static Brush bNaranja = new SolidColorBrush(Naranja);

        //Naranja = 231,126,55   => #E77E37
        //Verde    = 1,53,74         => #01354A
    }
}
