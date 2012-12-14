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
    /// Interaction logic for WLabs.xaml
    /// </summary>
    public partial class WLabs : Window
    {
        public WLabs()
        {
            InitializeComponent();
            tbCont.Background = Pantone.bVerde;
        }
        Laboratorio lab;
        AppInfo appInfo = new AppInfo();

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CargaLabs();            
        }

        private void CargaLabs()
        {
            LaboratorioBLL _labs = new LaboratorioBLL();
            cbDisp.ItemsSource = _labs.ObtenerAll();
        }

        private void cbDisp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            lab = (Laboratorio)cbDisp.SelectedValue;
            if (lab != null)
            {
                txtNomBL.Text = lab.Nombre;
                txtResB.Text = lab.Responsable;
            }
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Validar(txtNomBL, txtResB))
            {
                LaboratorioBLL labs = new LaboratorioBLL();
                lab.Nombre = txtNomBL.Text;
                lab.Responsable = txtResB.Text;
                if (labs.Modificar(lab))
                {
                    MessageBox.Show(this, "Laboratorio Modificado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.CargaLabs();
                    Utilidades.Limpiar(txtNomBL, txtResB);
                }
                else
                {
                    MessageBox.Show(this, "No se puede eliminar ya que hay registro que depende de este", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
                MessageBox.Show(this,"No debe haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EliminarLab(object sender, RoutedEventArgs e)
        {
            LaboratorioBLL labs = new LaboratorioBLL();
            if (labs.Eliminar(lab))
            {
                MessageBox.Show(this,"Laboratorio Eliminado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                Utilidades.Limpiar(txtNomBL, txtResB);
                this.CargaLabs();
            }
            else
                MessageBox.Show(this,"Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AgregarLab(object sender, RoutedEventArgs e)
        {            
            if (Utilidades.Validar(txtNomL,txtRes))
            {
                LaboratorioBLL labs = new LaboratorioBLL();
                lab = new Laboratorio()
                {
                    Nombre = txtNomL.Text,
                    Responsable = txtRes.Text
                };
                if (labs.Agregar(lab) != -1)
                {
                    MessageBox.Show(this,"Laboratorio Agregado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.CargaLabs();
                    Utilidades.Limpiar(txtNomL, txtRes);
                }
                else
                    MessageBox.Show(this,"Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show(this,"No debe de haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void txtRes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.AgregarLab(null, null);

        }
    }
}