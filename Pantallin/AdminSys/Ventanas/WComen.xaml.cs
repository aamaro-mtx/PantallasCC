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
    /// Interaction logic for WComen.xaml
    /// </summary>
    public partial class WComen : Window
    {
        public WComen()
        {
            InitializeComponent();
        }
        Aviso _aviso;
        AppInfo appInfo = new AppInfo();

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CargaAvisos();
        }

        private void CargaAvisos()
        {
            AvisoBLL avisos = new AvisoBLL();
            cbDisp.ItemsSource = avisos.ObtenerAll();
        }

        private void cbDisp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            _aviso = (Aviso)cbDisp.SelectedValue;
            if (_aviso != null)
            {
                txtAvi2.Text = _aviso.Noticia;
            }
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Validar(txtAvi2))
            {
                AvisoBLL avisos = new AvisoBLL();
                _aviso.Noticia = txtAvi2.Text;
                if (avisos.Modificar(_aviso))
                {
                    MessageBox.Show(this, "Aviso Modificado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.CargaAvisos();
                    Utilidades.Limpiar(txtAvi2);
                }
                else
                    MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show(this, "No debe haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EliminarLab(object sender, RoutedEventArgs e)
        {
            AvisoBLL avisos = new AvisoBLL();
            if (avisos.Eliminar(_aviso))
            {
                MessageBox.Show(this, "Aviso Eliminado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                Utilidades.Limpiar(txtAvi2);
                this.CargaAvisos();
            }
            else
                MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AgregarLab(object sender, RoutedEventArgs e)
        {

            if (Utilidades.Validar(txtAvi))
            {
                AvisoBLL avisos = new AvisoBLL();
                _aviso = new  Aviso();
                _aviso.Noticia = txtAvi.Text;
                if (avisos.Agregar(_aviso) != -1)
                {
                    MessageBox.Show(this, "Aviso Agregado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.CargaAvisos();
                    Utilidades.Limpiar(txtAvi);
                }
                else
                    MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show(this, "No debe de haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void txtRes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.AgregarLab(null, null);
        }
    }
}
