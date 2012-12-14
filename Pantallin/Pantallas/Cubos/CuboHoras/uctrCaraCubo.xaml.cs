using Entidades;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using Cubo.Modelo;

namespace ControlHorario
{
    public partial class uctrCaraCubo : UserControl
    {
        private Horario _ImgActual;

        public Horario ImgActual
        {
            get { return _ImgActual; }
            set
            {
                if (_ImgActual != value)
                {
                    _ImgActual = value;
                    ChangeImage();
                }

            }
        }

        private void ChangeImage()
        {
            if (_ImgActual != null)
            {
                CargaCara();
            }
        }

        private void CargaCara()
        {
            lblDocente.Text = _ImgActual.Docente.Nombre;
            lblCarrera.Text = _ImgActual.Grupo.Carrera;
            lblGrupo.Text = _ImgActual.Grupo.Nombre;
            string r = AppDomain.CurrentDomain.BaseDirectory;
            lblHora.Text = string.Format("HORARIO {0} - {1}", _ImgActual.HoraInicio, _ImgActual.HoraFin);
            lblMateria.Text = _ImgActual.Asignatura.Nombre;
            string ruta = _ImgActual.Docente.RutaImg;
            if (!string.IsNullOrEmpty(ruta) && System.IO.File.Exists(ruta))
                imgDocente.Source = (new BitmapImage
                    (new Uri(_ImgActual.Docente.RutaImg, UriKind.RelativeOrAbsolute)));
            else
            {

                string rut = System.IO.Path.Combine(r, "Imagenes\\imagen.jpg");
                imgDocente.Source = new BitmapImage(new Uri(rut));
            }
            this.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(r, "Imagenes\\fondo.jpg"))));
        }

        public uctrCaraCubo()
        {
            InitializeComponent();
        }

        private BitmapImage ObtenerImagen(byte[] bytex)
        {
            try
            {
                BitmapImage bitImg = new BitmapImage();
                bitImg.BeginInit();
                using (System.IO.Stream ms = new System.IO.MemoryStream(bytex))
                {
                    bitImg.StreamSource = ms;
                    bitImg.EndInit();
                }
                return bitImg;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se puede obtener la imagen " + ex.Message);
                return null;
            }

        }

        private void caraCubo_Loaded_1(object sender, RoutedEventArgs e)
        {
            //if (ImgActual != null)
            //{
            //    CargaCara();
            //}
            //MessageBox.Show("iniciando cuboHora");

        }
    }
}
