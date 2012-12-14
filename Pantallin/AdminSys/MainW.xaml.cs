using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HorarioS;

namespace PUsuarios
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Labs_Click(object sender, RoutedEventArgs e)
        {
            WLabs labs = new WLabs();
            labs.ShowDialog();
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            WDoc docs = new WDoc();
            docs.ShowDialog();
        }

        private void Cur_Click(object sender, RoutedEventArgs e)
        {
            //WCur curs = new WCur();
            //curs.ShowDialog();
        }

        private void Gru_Click(object sender, RoutedEventArgs e)
        {
            WGrup grups = new WGrup();
            grups.ShowDialog();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Asig_Click(object sender, RoutedEventArgs e)
        {
            WAsig asig = new WAsig();
            asig.ShowDialog();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            mnPrin.Background = Pantone.bNaranja;
        }

        private void Horarios_Click(object sender, RoutedEventArgs e)
        {
            VHorario hora = new VHorario();
            //ImageBrush img = new ImageBrush();
            //img.ImageSource = "";
            ////hora.Background = new ImageB);
            
            ////hora.Background = new ImageBrush((
            hora.ShowDialog();
        }

        private void Comentarios_Click(object sender, RoutedEventArgs e)
        {
            WComen comentarios = new WComen();
            comentarios.ShowDialog();
        }

        private void SalaJuntas_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
