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
    /// <summary>
    /// Interaction logic for WGrup.xaml
    /// </summary>
    public partial class WGrup : Window
    {
        public WGrup()
        {
            InitializeComponent();
            tbPri.Background = Pantone.bVerde;
        }
        Grupo grup;
        AppInfo appInfo = new AppInfo();
        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            var x = Utilidades.Validar(txtNom2);
           
            if (x && !string.IsNullOrEmpty(cbCar2.Text))
            {
                GrupoBLL grupos = new GrupoBLL();
                //var g = (Curso)cbCur2.SelectedValue;
                grup.Nombre = txtNom2.Text;
                grup.Carrera = cbCar2.Text;
                //grup.IDCurso = g.ID;
                if (grupos.Modificar(grup))
                {
                    MessageBox.Show(this, "Grupo Modificado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.cargaCombos();
                    Utilidades.Limpiar(txtNom2);
                }
                else
                    MessageBox.Show(this,"Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show(this,"No debe de haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminaGrupo(object sender, RoutedEventArgs e)
        {
            GrupoBLL grupos = new GrupoBLL();
            if (grupos.Eliminar(grup))
            {
                MessageBox.Show(this,"Grupo Eliminado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                Utilidades.Limpiar(txtNom2);
                this.cargaCombos();
            }
            else
                MessageBox.Show(this,"Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbDisp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            grup = (Grupo)cbDis.SelectedValue;
            if (grup != null)
            {
                txtNom2.Text = grup.Nombre;
                //cbCur2.Text = grup.IDCurso.ToString();
                cbCar2.Text = grup.IDCurso.ToString();
            }            
        }

        private void AgregaGrupo(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Validar(txtNom) && !string.IsNullOrEmpty(cbCar1.Text))
            {
                GrupoBLL grupos = new GrupoBLL();
                //var x = (Curso)cbCurso.SelectedValue;

                grup = new Grupo()
                {
                    Nombre = txtNom.Text,
                    Carrera = cbCar1.Text,
                    IDCurso = 1
                };

                if (grupos.Agregar(grup) != -1)
                {
                    MessageBox.Show(this, "Grupo Agregado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.cargaCombos();
                    Utilidades.Limpiar(txtNom);
                }
                else
                    MessageBox.Show(this,"Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(this,"No debe haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.cargaCombos();
        }

        private void cargaCombos()
        {
            CursoBLL cursos = new CursoBLL();
            CarreraBLL carreras = new CarreraBLL();
            GrupoBLL grupos = new GrupoBLL();
            cbCar1.ItemsSource = carreras.ObtenerAll();
            //cbCurso.ItemsSource = cursos.ObtenerAll();
            cbCar2.ItemsSource = carreras.ObtenerAll();
            //cbCur2.ItemsSource = cursos.ObtenerAll();
            cbDis.ItemsSource = grupos.ObtenerAll();
        }
    }
}
