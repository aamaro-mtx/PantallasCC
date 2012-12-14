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
using System.Windows.Shapes;
using Entidades;
using Negocio;

namespace PUsuarios
{
    public partial class WCur : Window
    {
        public WCur()
        {
            InitializeComponent();            
        }

        Curso _curso;

        List<string> Meses = new List<string>()
        {
          "NULL", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO",
            "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"
        };

        private void AgregaCurso(object sender, RoutedEventArgs e)
        {
            CursoBLL cursos = new CursoBLL();
            DateTime? inicio = calInicio.SelectedDate;
            DateTime? fin = calFin.SelectedDate;

            if (inicio.HasValue && fin.HasValue)
            {
                _curso = new Curso()
                {
                    MesInicio = Meses[inicio.Value.Month],
                    MesFin = Meses[fin.Value.Month],
                    Año = inicio.Value.Year,
                    Fase = (bool)(rb_fas1.IsChecked) ? 1 : 2
                };
                try
                {
                    cursos.Agregar(_curso);
                    MessageBox.Show("Curso Agregado Correctamente");
                    this.CargaCursos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }
            else            
                MessageBox.Show("Selecccione fechas Validas, OK");
            
           
        }


        private void EliminaCurso(object sender, RoutedEventArgs e)
        {
            CursoBLL cursos = new CursoBLL();
            if (cursos.Eliminar(_curso))
            {
                MessageBox.Show("Curso Eliminado, OK ");
                this.CargaCursos();
            }
            else
                MessageBox.Show("Se produjo un Error, Fail");
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            DateTime? inicio = calInicio.SelectedDate;
            DateTime? fin = calFin.SelectedDate;
            CursoBLL cursos = new CursoBLL();

            _curso.MesInicio = Meses[inicio.Value.Month];
            _curso.MesFin = Meses[fin.Value.Month];
            _curso.Año = inicio.Value.Year;
            _curso.Fase = (bool)(rb_fas1.IsChecked) ? 1 : 2;
            
            if (cursos.Modificar(_curso))
            {
                MessageBox.Show("Curso Modificado, OK ");
                this.CargaCursos();
            }
            else
                MessageBox.Show("Se produjo un Error, Fail");
        }

        private void cbDisp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            _curso = (Curso)cb_Dis.SelectedValue;
            
            try
            {                
                DateTime? inicio = new DateTime(_curso.Año,Meses.IndexOf(_curso.MesInicio), 1);
                DateTime? fin = new DateTime(_curso.Año, Meses.IndexOf(_curso.MesFin), 1);

                if (_curso != null)
                {
                    calInicio2.SelectedDate = inicio;
                    calFin2.SelectedDate = fin;
                    rbFaz1.IsChecked = (_curso.Fase == 1) ? true : false;
                }
                else
                    MessageBox.Show("Ya agrego  ? ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.CargaCursos();
            //this.Background = Pantone.bNaranja;
        }

        private void CargaCursos()
        {
            CursoBLL cursos = new CursoBLL();
            cb_Dis.ItemsSource = cursos.ObtenerAll();
            //cb_Dis.DisplayMemberPath = _curso.MesInicio + "-" + _curso.MesFin;
        }
    }
}
