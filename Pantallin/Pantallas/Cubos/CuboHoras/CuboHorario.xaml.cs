using Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Timer = System.Windows.Threading.DispatcherTimer;
//using System.Data.Objects;
//using Cubo.Modelo;

namespace ControlHorario
{
    public partial class CuboHorario : Window
    {
        public CuboHorario(List<Horario> imgs)
        {
            InitializeComponent();
            Imagenes = imgs;
        }

        int index = 0;
        Timer tim = new Timer();
        List<Horario> Imagenes = new List<Horario>();
  
        #region Tratamineto de Imagenes

        private BitmapImage ObtenerImagen(byte[] bytex)
        {
            try
            {
                BitmapImage bitImg = new BitmapImage();
                bitImg.BeginInit();
                using (
                System.IO.Stream ms = new System.IO.MemoryStream(bytex))
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
        #endregion

        private void escribeTexto()
        {
            string texto = "";
            FormattedText frmTxt = new FormattedText(texto,
                 CultureInfo.GetCultureInfo("en-us"),
        FlowDirection.LeftToRight, new Typeface("Verdana"), 32, Brushes.Black);
            // Set a maximum width and height. If the text overflows these values, an ellipsis "..." appears.
            frmTxt.MaxTextWidth = 300;
            frmTxt.MaxTextHeight = 240;

            // Use a larger font size beginning at the first (zero-based) character and continuing for 5 characters.
            // The font size is calculated in terms of points -- not as device-independent pixels.
            frmTxt.SetFontSize(36 * (96.0 / 72.0), 0, 5);

            // Use a Bold font weight beginning at the 6th character and continuing for 11 characters.
            frmTxt.SetFontWeight(FontWeights.Bold, 6, 11);

            // Use a linear gradient brush beginning at the 6th character and continuing for 11 characters.
            frmTxt.SetForegroundBrush(
                                    new LinearGradientBrush(
                                    Colors.Orange,
                                    Colors.Teal,
                                    90.0),
                                    6, 11);

            // Use an Italic font style beginning at the 28th character and continuing for 28 characters.
            frmTxt.SetFontStyle(FontStyles.Italic, 28, 28);

            //// Draw the formatted text string to the DrawingContext of the control.
            //drawingContext.DrawText(frmTxt, new Point(10, 0));
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                tim.Interval = new TimeSpan(0, 0, 0);
                tim.Tick += tim_Tick;
                tim.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error >.<¡ " + ex.Message);
            }
        }

        private void tim_Tick(object sender, EventArgs e)
        {
            tim.Interval = new TimeSpan(0, 0, 10);
            if (index >= Imagenes.Count)
            {
                index = 0;
                tim.Stop();
                this.Close();
            }
            CaractrCubo.ImgActual = Imagenes[index];
            //lblLab.Content = string.Format("LABORATORIO {0} {1} " , Imagenes[index].Laboratorio.ID, Imagenes[index].Laboratorio.Nombre);
            lblNoLab.Content = Imagenes[index].Laboratorio.ID;
            index++;
        }

        private void lblNoLab_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            Control tmp = sender as Control;
            tmp.FontSize = (e.NewSize.Height - e.NewSize.Height * .05d) / tmp.FontFamily.LineSpacing;            
        }
    }

}
