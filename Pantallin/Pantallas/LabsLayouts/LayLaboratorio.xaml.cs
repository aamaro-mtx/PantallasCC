using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace ControlHorario
{
    public partial class LayLaboratorio : Window
    {
        Timer tim = new Timer();
        public LayLaboratorio(int _lab)
        {
            InitializeComponent();
            
            tim.Interval = new TimeSpan(0, 0, 10);
            tim.Tick += tim_Tick;
            tim.Start();
            noLab = _lab;
        }

        private int noLab;

        private void tim_Tick(object sender, EventArgs e)
        {
            tim.Stop();
            this.Close();          
        }

        private void cargaDelaVentana(object sender, RoutedEventArgs e)
        {
            Dibujar dib = new Dibujar();
            inicializaVentana();
            dib.dibujaLaboratorio(noLab, gridPadre);
        }

        private void inicializaVentana()
        {
            var pan = Screen.PrimaryScreen;
            int anc = pan.WorkingArea.Width -100;
            int lar = pan.WorkingArea.Height -100;
            int x = (pan.WorkingArea.Width - anc) / 2;
            int y = (pan.WorkingArea.Height - lar) / 2;
            rectImagen.Width = anc;
            rectImagen.Height = lar;
            //Canvas.SetLeft(rectImagen, x);
            //Canvas.SetTop(rectImagen, y);
            Canvas.SetLeft(LayoutGrid, x);
            Canvas.SetTop(LayoutGrid, y);

            Random rnd = new Random((int)DateTime.Now.Ticks);
            double aleatorio = rnd.NextDouble();
            if (aleatorio >= 0.5)
                Rotar(aleatorio * 10);
            else
                Rotar(aleatorio * -10);
        }

        private void Rotar(double p)
        {
            RotateTransform rotTra = new RotateTransform();
            TransformGroup tranGrp = new TransformGroup();
            rotTra.Angle = p;
            tranGrp.Children.Add(rotTra);            
            rectImagen.RenderTransform = tranGrp;
            gridPadre.RenderTransform = tranGrp;
        }

        private void LayoutGrid_Loaded_1(object sender, RoutedEventArgs e)
        {
            Storyboard animAparecer = (Storyboard)FindResource("TimelineAparecer");
            animAparecer.Begin(this);
        }

        #region codigo Comentado
        //public void funcionDibujar(TabControl tab, int? NLab = null)
        //{            
        //    Dibujar d = new Dibujar();
        //    if (NLab == null)
        //    {
        //        d.DibujaLayout(tab);
        //    }
        //    else
        //    {
        //        tab.Items.Add(d.DibujaLaboratorio(NLab.Value));
        //        tab.Items.Add(new TabItem());
        //    }
        //    tab.Visibility = Visibility.Visible;
            
        //}

        //private void cargaDelaVentana(object sender, RoutedEventArgs e)
        //{
        //    funcionDibujar(padre, Lab);
        //}

        //private void Window_Loaded_1(object sender, RoutedEventArgs e)
        //{

        //}

        //private void LimpiaCanvas(object sender, RoutedEventArgs e)
        //{

        //}

        //private void funcionDibujar(object sender, RoutedEventArgs e)
        //{
        //    funcionDibujar(padre,Lab);
        //    //funcionDibujar(
        //    //Dibujar d = new Dibujar();
        //    ////if (NLab == null)
        //    ////{
                
        //    ////}
        //    //var nLab = int.Parse(txtLab.Text);
        //    //padre.Items.Add(d.DibujaLaboratorio(nLab));
        //    //padre.Items.Add(new TabItem());

        //    //pnlMst.Margin = new Thickness(padre.Margin.Right, 0, 0, 0);
        //    //pnlMst.Visibility = Visibility.Visible;
               
       
        //}

        //private void funcLimpiar(object sender, RoutedEventArgs e)
        //{
        //    padre.Items.Clear();
        //}

        //private void ajustaGrilla(object sender, RoutedEventArgs e)
        //{
        //    //string[] split = txtMar.Text.Split(',');
        //    //img.Margin = new Thickness(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]));
        //}
        #endregion

    }
}
