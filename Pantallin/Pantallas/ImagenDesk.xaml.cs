using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ControlHorario
{
    public partial class MainWindow : Window
    {
        #region Miembros
        private int izq = 50, arr = 50;

        private double incrementoAnteriorX = 0;
        private double incrementoAnteriorY = 0;

        private bool mousePresionado;

        private Point coordenadaClic;
        private double x = 0;
        private double y = 0;
        private double deltaX = 0;
        private double deltaY = 0;

        List<Imagen> listaImagenes;
        #endregion

        
        public MainWindow()
		{
			this.InitializeComponent();
						
            AdministradorSuperficie.ImagenActiva = null;
            AdministradorSuperficie.Superficie = this;
            listaImagenes = new List<Imagen>();
            mousePresionado = false;
		}

        private void btnAgregar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RotarX(45);
        }

        private void AgregarControl(int id,Asignatura asig)
        {
            Imagen nuevaImagen = new Imagen();
            nuevaImagen.Width = 800;

            AdministradorSuperficie.ImagenActiva = nuevaImagen;
            LayoutRoot.Children.Add(AdministradorSuperficie.ImagenActiva);

            AdministradorSuperficie.ImagenActiva.Orden = LayoutRoot.Children.Count;
            listaImagenes.Add(nuevaImagen);
            ReordenarImagenes();

            Canvas.SetLeft(AdministradorSuperficie.ImagenActiva, 50);
            Canvas.SetTop(AdministradorSuperficie.ImagenActiva, 50);

            Random rnd = new Random((int)DateTime.Now.Ticks);
            double aleatorio = rnd.NextDouble();
            if (aleatorio >= 0.5)
                AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * 20);
            else
                AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * -20);
        }

        private void AgregarControl(bool estado)
        {
            if (estado)
            {                
                LaboratorioBLL service = new LaboratorioBLL();
                List<Laboratorio> lstLab = service.ObtenerAll();

                if (lstLab != null)
                {
                    foreach (var lab in lstLab)
                    {
                        #region Creacion  y parametrizacion de izq y arrb
                        Imagen nuevaImagen = new Imagen(lab.Responsable);

                        AdministradorSuperficie.ImagenActiva = nuevaImagen;
                        LayoutRoot.Children.Add(AdministradorSuperficie.ImagenActiva);

                        AdministradorSuperficie.ImagenActiva.Orden = LayoutRoot.Children.Count;
                        listaImagenes.Add(nuevaImagen);
                        ReordenarImagenes();

                        Canvas.SetLeft(AdministradorSuperficie.ImagenActiva, izq);
                        Canvas.SetTop(AdministradorSuperficie.ImagenActiva, arr);

                        Random rnd = new Random((int)DateTime.Now.Ticks);
                        double aleatorio = rnd.NextDouble();
                        if (aleatorio >= 0.5)
                            AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * 20);
                        else
                            AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * -20);
                        #endregion

                        int ancho = (int)nuevaImagen.Width + 10;
                        int largo = (int)nuevaImagen.Height + 50;
                        izq += ancho;
                        if (lab.ID % 3 == 0)
                        {
                            arr += largo + 20;
                            izq = 50;
                        }
                    }
                }
                else
                    System.Windows.Forms.MessageBox.Show("Error :(");
            }
        }              

        private void RotarX(int grados)
        {
            Random r=new Random();

            foreach (Imagen img in listaImagenes)
                img.Rotar(r.Next(1, 100));
                //img.Rotar(grados);
        }
        
        private void btnMezclar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);

            foreach (Imagen img in listaImagenes)
            {
                double aleatorio = rnd.NextDouble();
                if (aleatorio >= 0.5)
                    img.Rotar(aleatorio * 20);
                else
                    img.Rotar(aleatorio * -20);
            }
        }

        public void TraerAlFrenteImagenActiva()
        {
            AdministradorSuperficie.ImagenActiva.Orden = int.MaxValue;
            ReordenarImagenes();
        }

        private void ReordenarImagenes()
        {
            listaImagenes.Sort(delegate(Imagen img1, Imagen img2) { return img1.Orden.CompareTo(img2.Orden); });

            for (int i = 0; i < listaImagenes.Count; i++)
            {
                listaImagenes[i].Orden = i + 1;
                Canvas.SetZIndex(listaImagenes[i], i + 1);
            }
        }

        #region Eventos Ventana
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mousePresionado = true;

            if (AdministradorSuperficie.ImagenActiva != null)
            {
                coordenadaClic = e.GetPosition(LayoutRoot);

                deltaX = Canvas.GetLeft(AdministradorSuperficie.ImagenActiva);
                deltaY = Canvas.GetTop(AdministradorSuperficie.ImagenActiva);

                TraerAlFrenteImagenActiva();
            }
        }

        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mousePresionado = false;
            if (AdministradorSuperficie.ImagenActiva != null)
                AdministradorSuperficie.ImagenActiva.InicializarControl();
    
            incrementoAnteriorX = 0;
            incrementoAnteriorY = 0;
        }

        protected void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(LayoutRoot);

            if (mousePresionado && AdministradorSuperficie.ImagenActiva != null)
            {
                if (AdministradorSuperficie.ImagenActiva.EstaRotando)
                {
                    AdministradorSuperficie.ImagenActiva.Rotar(p.X, p.Y);
                }
                else if (AdministradorSuperficie.ImagenActiva.EstaMoviendo)
                {
                    x = deltaX + p.X - coordenadaClic.X;
                    y = deltaY + p.Y - coordenadaClic.Y;

                    Canvas.SetLeft(AdministradorSuperficie.ImagenActiva, x);
                    Canvas.SetTop(AdministradorSuperficie.ImagenActiva, y);

                    AdministradorSuperficie.ImagenActiva.ActualizarCentro();
                }
                else if (AdministradorSuperficie.ImagenActiva.EstaEscalando)
                {
                    double anteriorX = Canvas.GetLeft(AdministradorSuperficie.ImagenActiva);
                    double anteriorY = Canvas.GetTop(AdministradorSuperficie.ImagenActiva);

                    AdministradorSuperficie.ImagenActiva.Escalar(p.X - coordenadaClic.X - incrementoAnteriorX, p.Y - coordenadaClic.Y - incrementoAnteriorY);

                    Canvas.SetLeft(AdministradorSuperficie.ImagenActiva, anteriorX - (p.X - coordenadaClic.X - incrementoAnteriorX));
                    Canvas.SetTop(AdministradorSuperficie.ImagenActiva, anteriorY - (p.Y - coordenadaClic.Y - incrementoAnteriorY));

                    incrementoAnteriorX = p.X - coordenadaClic.X;
                    incrementoAnteriorY = p.Y - coordenadaClic.Y;
                }
            }
        }
        #endregion

        private void btnOrdenar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (Imagen img in listaImagenes)
                img.Rotar(0);
        }

        private void CrearTodas(object sender, RoutedEventArgs e)
        {        
           AgregarControl(true);                
        }

        private void Prueba(object sender, RoutedEventArgs e)
        {
            Imagen ventana = new Imagen("Ingenieria en sistemas");
            AsignaturaBLL _asigaturas = new AsignaturaBLL();

            Asignatura _asigna = _asigaturas.ObtenerAll().FirstOrDefault(p => p.ID == 1); 
                    
            AgregarControl(1,_asigna);
        }
    }
}
