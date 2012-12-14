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
using System.ComponentModel;

namespace Entidades
{
    /// <summary>
    /// Lógica de interacción para Computadoras.xaml
    /// </summary>
    public partial class uctComputadora : UserControl, INotifyPropertyChanged
    {
        private bool isLocked;
        private string usuario = "Disponible";
        private int numeroLab;
        private int numeroEquipo;
        private int idSesion;
        private Estado estado = Estado.Disponible;

        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        public int SessionID
        {
            get { return idSesion; }
            set
            {
                idSesion = value;
                OnPropertyChanged("SessionID");
            }
        }

        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        public int Numero
        {
            get { return numeroEquipo; }
            set
            {
                numeroEquipo = value;
                OnPropertyChanged("Numero");
            }
        }

        public int Laboratorio
        {
            get { return numeroLab; }
            set
            {
                numeroLab = value;
                OnPropertyChanged("Laboratorio");
            }
        }

        public Estado EstadoEquipo
        {
            get { return estado; }
            set
            {
                if (estado != value)
                {
                    estado = value;
                    ChangeColors();
                }

            }
        }

        public uctComputadora()
        {
            InitializeComponent();
        }

        public uctComputadora(int numeroEquipo)
        {
            InitializeComponent();
            this.numeroEquipo = numeroEquipo;
        }

        public uctComputadora(int numeroLab, int numeroEquipo)
        {
            InitializeComponent();
            this.numeroLab = numeroLab;
            this.numeroEquipo = numeroEquipo;
        }

        public enum Estado
        {
            Apagada = 0,
            Disponible = 1,
            Ocupada = 2
        };

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private void ChangeColors()
        {
            LinearGradientBrush gradiente = new LinearGradientBrush();
            gradiente.StartPoint = new Point(0, 0.5);
            gradiente.EndPoint = new Point(1, 0.5);
            switch (EstadoEquipo)
            {
                case Estado.Apagada:
                    gradiente.GradientStops.Add(new GradientStop(Colors.DarkSlateBlue, 0.0));
                    gradiente.GradientStops.Add(new GradientStop(Colors.CadetBlue, 0.0));
                    break;
                case Estado.Disponible:
                    gradiente.GradientStops.Add(new GradientStop(Colors.DarkGreen, 0.0));
                    gradiente.GradientStops.Add(new GradientStop(Colors.Chartreuse, 1.0));
                    break;
                case Estado.Ocupada:
                    gradiente.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
                    gradiente.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
                    break;
                default:
                    break;
            }
            if (gradiente != null)
            {
                this.Contenido.Background = gradiente;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
