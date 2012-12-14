using System;
using System.Windows;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace ControlHorario
{
    public partial class CuboWin : Window
    {
        public Uri Ruta { get; set; }
        Timer tim = new Timer();

        public CuboWin(Uri ruta)
        {
            Ruta = ruta;
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {            
            //string r = AppDomain.CurrentDomain.BaseDirectory;
            //Ruta = new Uri(r + @"Imagenes\AvisosCC.png", UriKind.RelativeOrAbsolute);
            Media.Source = Ruta;
            tim.Interval = new TimeSpan(0, 0, 10);
            tim.Tick += tim_Tick;
            tim.Start();
            //cargaRutas();
            lblTest.Content = "Iniciando Cubo" + DateTime.Now.ToString() +" "+ Ruta;
        }

        private void tim_Tick(object sender, EventArgs e)
        {
            tim.Stop();
            this.Close();
        }
    }
}
