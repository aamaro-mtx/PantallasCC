using System;
using System.Collections.Generic;
using System.Text;

namespace ControlHorario
{
    /// <summary>
    /// Al AdministradorSuperficie se encarga de mantener la referencia entre el control de usuario y la ventan principal.
    /// </summary>
    public class AdministradorSuperficie
    {
        private static Imagen imagenActiva;
        //private static ImageDesk.Window1 superficie;
        private static ControlHorario.MainWindow superficie;

        // Representa la ventana principal
        public static ControlHorario.MainWindow Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }

        // Representa el control seleccionado.
        public static Imagen ImagenActiva
        {
            get { return imagenActiva; }
            set { imagenActiva = value; }
        }

        // Remueve el control de la colección de objetos de la ventana.
        public static void CerrarVentanaActiva(Imagen ventana)
        {
            superficie.LayoutRoot.Children.Remove(ventana);
        }

    }
}
