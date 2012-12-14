using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Entidades;


namespace ControlHorario
{
    public partial class Imagen
    {
        #region agregado 18/092012
        public Asignatura Asignatura1 { get; set; }
        public int Index { get; set; }

        #endregion

        #region Miembros
        private TransformGroup grupoElementos;
        private RotateTransform rotacionImagen;
        private ImageBrush imagen;

        private bool rotando;
        private bool moviendo;
        private bool escalando;
        private bool cerrando;

        private double centroX;
        private double centroY;

        private double anguloAjuste = -1;
        private double ultimoAngulo = 0;

        private int orden;

        private string contenido;
        #endregion

        #region Propiedades
        public Horario HorarioActual { get; set; }

        public double AnguloAjuste
        {
            get { return anguloAjuste; }
            set { anguloAjuste = value; }
        }

        public int Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        public bool EstaRotando
        {
            get { return rotando; }
            set { rotando = value; }
        }

        public bool EstaMoviendo
        {
            get { return moviendo; }
            set { moviendo = value; }
        }

        public bool EstaEscalando
        {
            get { return escalando; }
            set { escalando = value; }
        }

        public string Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }
        #endregion

        #region Constructores

        public Imagen(int id,Asignatura asig)
        {
            this.Index = id;
            this.Asignatura1 = asig;
            this.InicializadorImagen();
        }
        public Imagen(string mensaje)
        {
            Contenido = mensaje;
            this.InicializadorImagen();
        }

     
        public Imagen()
        {
            InicializadorImagen();
        }

        private void InicializadorImagen()
        {
            this.InitializeComponent();

            grupoElementos = new TransformGroup();
            rotacionImagen = new RotateTransform();
            imagen = new ImageBrush();

            rotacionImagen.CenterX = this.Width / 2;
            rotacionImagen.CenterY = this.Height / 2;
            grupoElementos.Children.Add(rotacionImagen);

            RenderTransform = grupoElementos;
            orden = 0;

            InicializarControl();
        }

        #endregion

        private void LayoutRoot_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Storyboard animAparecer = (Storyboard)FindResource("TimelineAparecer");
            animAparecer.Begin(this);
        }

        #region Manejadores de Eventos

        private void rectImagen_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Notificar al administrador de la superficie que seleccione esta imagen.
            AdministradorSuperficie.ImagenActiva = this;
        }

        private void recGiro_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Notificar al administrador de la superficie que seleccione esta imagen.
            AdministradorSuperficie.ImagenActiva = this;
            rotando = true;
        }

        private void recGiro_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Desactiva la modalidad de girar el control.
            rotando = false;
            anguloAjuste = -1;
        }

        private void LayoutRoot_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Activa el modo de movimeinto del control.
            if (!escalando && !cerrando)
                moviendo = true;
        }

        private void LayoutRoot_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Desactiva el modo de movimiento del control.
            moviendo = false;
        }

        private void rectEscala_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!moviendo && !cerrando)
            {
                // Notificar al administrador de la superficie que seleccione esta imagen.
                AdministradorSuperficie.ImagenActiva = this;
                escalando = true;
            }
        }

        private void rectEscala_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Desactiva el modo de escalar el control.
            escalando = false;
            ActualizarCentro();
        }

        private void rectCerrar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            cerrando = true;
        }

        private void rectCerrar_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Iniciar la animación de cierre de la ventana.
            if (cerrando)
            {
                Storyboard animCerrar = (Storyboard)FindResource("TimelineCerrar");
                animCerrar.Completed += new EventHandler(animCerrar_Completed);
                animCerrar.Begin(this);
            }
        }

        void animCerrar_Completed(object sender, EventArgs e)
        {
            // Notificar al administrador de la superficie que cierre esta imagen.
            AdministradorSuperficie.CerrarVentanaActiva(this);
        }

        private void txtNombre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //if (lblNombre != null)
            //    lblNombre.Text = txtNombre.Text;
        }

        #endregion

        #region Utilitarios

        public void InicializarControl()
        {
            rotando = false;
            moviendo = false;
            escalando = false;
            cerrando = false;
            
        }

        public void InicializarControl(int id)
        {
            rotando = false;
            moviendo = false;
            escalando = false;
            cerrando = false;
            Index = id;
        }

        public void ActualizarCentro()
        {
            // Recalcula el centro de la imagen en función de la posición y tamaño del control.
            centroX = Canvas.GetLeft(this) + this.Width / 2;
            centroY = Canvas.GetTop(this) + this.Height / 2;

            rotacionImagen.CenterX = this.Width / 2;
            rotacionImagen.CenterY = this.Height / 2;
        }


        public void Escalar(double ancho, double alto)
        {
            // Redimensiona el control.
            double nuevoAncho = this.Width + ancho * 2;
            double nuevoAlto = this.Height + alto * 2;

            if (nuevoAncho >= 0)
                this.Width = nuevoAncho;

            if (nuevoAlto >= 0)
                this.Height = nuevoAlto;

            anguloAjuste = -1;

            ActualizarCentro();
        }


        public void Rotar(double x, double y)
        {
            if (anguloAjuste == -1)
            {
                anguloAjuste = Math.Atan2(y - centroY, x - centroX) * (180 / Math.PI);
                anguloAjuste -= ultimoAngulo;
            }

            double angulo = Math.Atan2(y - centroY, x - centroX) * (180 / Math.PI) - anguloAjuste;

            Rotar(angulo);

        }

        public void Rotar(double angulo)
        {
            rotacionImagen.Angle = angulo;
            ultimoAngulo = rotacionImagen.Angle;
            this.RenderTransform = grupoElementos;
        }

        #endregion

        // Agregar texto al label 
        private void AgregaTexto(object sender, RoutedEventArgs e)
        {
            //lblNombre.Text = Contenido;           
        }

        private void AgregaContext(object sender, RoutedEventArgs e)
        {
            //lblPropiedad.Text = "44444444444444444444444";
        }

        private void rectImagen_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (HorarioActual != null)
            {
                lblCarrera.Text = HorarioActual.Grupo.Carrera.ToUpper();
                lblCarrera.FontSize = 35d;
                lblGrupo.Text = HorarioActual.Grupo.Nombre;
                lblMateria.Text = HorarioActual.Asignatura.Nombre;
                lblDocente.Text = HorarioActual.Docente.Nombre;
                lblHorario.Text = string.Format("Inicio: {0} - Fin: {1}", HorarioActual.HoraInicio, HorarioActual.HoraFin);
            }
            else
                MessageBox.Show("Se ha producido un error en la aplicaicon :(");
            //if (Asignatura1 != null)
            //{
            //    lblNombre.Text = Asignatura1.Nombre + " <-> " + this.Index;
            //    //lblPropiedad.Text = Asignatura1.Clave;
            //    //lblPropiedad.Text = Asignatura1.Laboratorio.Responsable;
            //}
            //else
            //{
            //    lblNombre.Text = "Indice --> "+Index;
            //    lblPropiedad.Text =Contenido;
            //}

        }

        private void LayoutRoot_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            Control tmp = sender as Control;
            if (tmp != null)
            {
                tmp.FontSize = e.NewSize.Height / tmp.FontFamily.LineSpacing;
            }
            else
                MessageBox.Show("aaaaaaa");
        }

    }
}