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
    /// Interaction logic for WAsig.xaml
    /// </summary>
    public partial class WAsig : Window
    {
        public WAsig()
        {
            InitializeComponent();
            
        }

        Asignatura _asignatura;
        AppInfo appInfo = new AppInfo();

        private void AgregaAsignatura(object sender, RoutedEventArgs e)
        {
            //bool hay = cbCur.SelectedValue != null;
            if (Utilidades.Validar(txtNom,txtCve) )
            {
                AsignaturaBLL asignas = new AsignaturaBLL();
                _asignatura = new Asignatura()
                {
                    IDCurso = 1,
                    //IDDocente = ((Docente)cbDoc.SelectedValue).ID,
                    //IDLab = ((Laboratorio)cbLab.SelectedValue).ID,
                    Nombre = txtNom.Text,
                    Clave = txtCve.Text
                };

                if (asignas.Agregar(_asignatura) != -1)
                {
                    MessageBox.Show(this, "Asignatura Agregada, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK,MessageBoxImage.Information);
                    this.cargaCombos();
                    Utilidades.Limpiar(txtCve, txtNom);
                }
                else
                    MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct, 
                        MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
                MessageBox.Show(this, "No debe de haber campos vacios"
                    , appInfo.AssemblyProduct, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.cargaCombos();
            //cbCur.SelectedIndex = 0;            
        }

        private void cargaCombos()
        {
            CursoBLL cursos = new CursoBLL();
            DocenteBLL docentes = new DocenteBLL();
            AsignaturaBLL asignaturas = new AsignaturaBLL();
           
            //cbCur.ItemsSource = cursos.ObtenerAll();          
            cbDisp.ItemsSource = asignaturas.ObtenerAll();
            //cbCur2.ItemsSource = cursos.ObtenerAll();       
        }

        private void EliminaAsignatura(object sender, RoutedEventArgs e)
        {
            AsignaturaBLL asignaturas = new AsignaturaBLL();
            if (asignaturas.Eliminar(_asignatura))
            {
                MessageBox.Show(this,"Asignatura Eliminada, OK ", appInfo.AssemblyProduct, 
                        MessageBoxButton.OK,MessageBoxImage.Information);
                this.cargaCombos();
                Utilidades.Limpiar(txtCve2, txtNom2);
            }
            else
                MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Validar(txtNom2, txtCve2))
            {
                AsignaturaBLL asignaturas = new AsignaturaBLL();
                _asignatura.Nombre = txtNom2.Text;
                _asignatura.Clave = txtCve2.Text;
                //_asignatura.IDCurso = ((Curso)cbCur2.SelectedValue).ID;
                //_asignatura.IDDocente = ((Docente)cbDoc2.SelectedValue).ID;
                //_asignatura.IDLab = ((Laboratorio)cbLab2.SelectedValue).ID;
                if (asignaturas.Modificar(_asignatura))
                {
                    MessageBox.Show(this,"Asignatura Modificada, OK ", appInfo.AssemblyProduct, 
                        MessageBoxButton.OK,MessageBoxImage.Information);
                    this.cargaCombos();
                    Utilidades.Limpiar(txtCve2, txtNom2);
                }
                else
                    MessageBox.Show(this,"Se produjo un Error, Fail", appInfo.AssemblyProduct, 
                        MessageBoxButton.OK,MessageBoxImage.Error);
            }else
                MessageBox.Show(this, "No debe de contener campos vacios", appInfo.AssemblyProduct,
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbDisp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            _asignatura = (Asignatura)cbDisp.SelectedValue;
            if (_asignatura != null)
            {
                txtNom2.Text = _asignatura.Nombre;
                txtCve2.Text = _asignatura.Clave;
            }           
        }

        private bool estanLlenos(ComboBox c1, ComboBox c2 = null)
        {
            if (c2 != null)
            {
                if (string.IsNullOrEmpty(c1.Text) ||
                    string.IsNullOrEmpty(c2.Text))
                {
                    return false;
                }
                else
                    return true;
            }
            else
                return string.IsNullOrEmpty(c1.Text);
        }

        private void TabItem_Unloaded_1(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Text ");
        }

        private void limpiarTabItem(object sender, RoutedEventArgs e)
        {
            //foreach (var item in contendor.Children)
            //{
            //    TextBox txt = item as TextBox;
            //    if (txt != null)
            //    {
            //        txt.Text = "";
            //    }
            //}
        }
    }
}
