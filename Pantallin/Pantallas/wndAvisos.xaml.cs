using Entidades;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Screen = System.Windows.Forms.Screen;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace ControlHorario
{
    /// <summary>
    /// Interaction logic for wndAvisos.xaml
    /// </summary>
    public partial class wndAvisos : Window
    {
        List<Aviso> lstAvisos = null;
        int index = 0;
        Timer temporizador = new Timer() { Interval = new TimeSpan(0, 0, 10) };
        double szFont = 42;

        public wndAvisos(List<Aviso> avisos)
        {
            InitializeComponent();
            lstAvisos = avisos;
            temporizador.Tick += temporizador_Tick;
            temporizador.Start();
        }

        private void temporizador_Tick(object sender, EventArgs e)
        {
            AgregarControl(lstAvisos[index]);
            index++;
            if (index >= lstAvisos.Count)
            {
                temporizador.Stop();
                this.Close();
                index = 0;
            }
            
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            AgregarControl(lstAvisos[0]);
            index++;
        }

        private void AgregarControl<T>(T Actual)
        {
            Imagen nuevaImagen;
            LayoutRoot.Children.Clear();
            Aviso avsActual = Actual as Aviso;
            var pan = Screen.PrimaryScreen;
            int anc = pan.WorkingArea.Width - 200;
            int lar = pan.WorkingArea.Height - 100;
            int x = (pan.WorkingArea.Width - anc) / 2;
            int y = (pan.WorkingArea.Height - lar) / 2;

            if (avsActual != null)
            {
                SolidColorBrush sbr = new SolidColorBrush(Color.FromRgb(1, 53, 74));
                using (nuevaImagen = new Imagen())
                {
                    #region Inicializador
                    nuevaImagen.Width = anc;
                    nuevaImagen.Height = lar;
                    nuevaImagen.lblNoticia.Text = avsActual.Noticia;
                    nuevaImagen.lblNoticia.Foreground = sbr;
                    nuevaImagen.lblNoticia.FontSize = szFont;

                    #region oculta
                    nuevaImagen.lblCarrera.Visibility = Visibility.Hidden;
                    nuevaImagen.lblGrupo.Visibility = Visibility.Hidden;
                    nuevaImagen.lblMateria.Visibility = Visibility.Hidden;
                    nuevaImagen.lblDocente.Visibility = Visibility.Hidden;
                    nuevaImagen.lblHorario.Visibility = Visibility.Hidden;
                    ImageBrush imgBr = new ImageBrush();
                    string r = AppDomain.CurrentDomain.BaseDirectory;
                    imgBr.ImageSource = (new BitmapImage
                        (new Uri(r + @"Imagenes\AvisosCC.png", UriKind.Relative)));
                    nuevaImagen.rectImagen.Fill = imgBr;
                    #endregion
                    #endregion
                    #region Acom odar
                    //AdministradorSuperficie.ImagenActiva.Dispose();
                    using (AdministradorSuperficie.ImagenActiva)
                    {
                        AdministradorSuperficie.ImagenActiva = nuevaImagen;
                        LayoutRoot.Children.Add(AdministradorSuperficie.ImagenActiva);

                        Canvas.SetLeft(AdministradorSuperficie.ImagenActiva, x);
                        Canvas.SetTop(AdministradorSuperficie.ImagenActiva, y);

                        Random rnd = new Random((int)DateTime.Now.Ticks);
                        double aleatorio = rnd.NextDouble();
                        if (aleatorio >= 0.5)
                            AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * 10);
                        else
                            AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * -10);
                    }
                    #endregion
                }
            }
        }

        private double calTamano(int a,int l,double lnSp)
        {
            var res = (l - l * .05d) / a;     
            return res;
        }

        private void LayoutRoot_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            //Control tmp = sender as Control;
            //double res = (e.NewSize.Height - e.NewSize.Height * .05d);
            //if (tmp != null)
            //{
            //    res = res / tmp.FontFamily.LineSpacing;
            //    tmp.FontSize = res;
            //    szFont = tmp.FontSize;
            //}
            //MessageBox.Show(res.ToString());
        }
    }
}
