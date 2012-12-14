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
using System.IO;
using Entidades;
using Negocio;


namespace PUsuarios
{
    /// <summary>
    /// Interaction logic for WDoc.xaml
    /// </summary>
    public partial class WDoc : Window
    {
        public WDoc()
        {
            InitializeComponent();
            tbPri.Background = Pantone.bVerde;
        }

        AppInfo appInfo = new AppInfo();
        byte[] FotoBytex = null;        
        Docente _docente;
        string nomImg, rutDes = @"C:\Fotos", arcOri, ext;

        private void AgregaDocente(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Validar(txtNom, txtDes))
            {
                DocenteBLL docentes = new DocenteBLL();
                _docente = new Docente()
                {
                    Nombre = txtNom.Text,
                    Descripcion = txtDes.Text,
                    RutaImg = almacenaImagen(txtNom.Text)
                };
                if (docentes.Agregar(_docente) != -1)
                {
                    MessageBox.Show(this, "Docente Agregado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.CargaDocentes();
                    Utilidades.Limpiar(txtDes, txtNom);
                    imgFoto2.Source = new BitmapImage(new Uri("Imagenes\\MrX.jpg", UriKind.RelativeOrAbsolute));
                    nomImg = string.Empty;
                }
                else
                    MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show(this,"No debe haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminaDocente(object sender, RoutedEventArgs e)
        {
            DocenteBLL docentes = new DocenteBLL();
            if (docentes.Eliminar(_docente))
            {
                MessageBox.Show(this, "Docente Eliminado, OK ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                this.CargaDocentes();
                Utilidades.Limpiar(txtNom2, txtDes2);
            }
            else
                MessageBox.Show(this, "Se produjo un Error, Fail", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Validar(txtNom2, txtDes2))
            {
                DocenteBLL docentes = new DocenteBLL();
                _docente.Nombre = txtNom2.Text;
                _docente.Descripcion = txtDes2.Text;
                if (FotoBytex != null)
                    _docente.RutaImg = almacenaImagen(_docente.Nombre);
          
                if (docentes.Modificar(_docente))
                {
                    MessageBox.Show(this,"Docente Modificado Correctamente", appInfo.AssemblyProduct,
                        MessageBoxButton.OK,MessageBoxImage.Information);
                    this.CargaDocentes();
                    Utilidades.Limpiar(txtDes2, txtNom2);
                    imgFoto2.Source = new BitmapImage(new Uri("Imagenes\\MrX.jpg", UriKind.RelativeOrAbsolute));
                    nomImg = string.Empty;
                }
                else
                    MessageBox.Show(this, "Se produjo un error", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show(this, "No debe haber campos vacios", appInfo.AssemblyProduct,
                        MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbDisp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            _docente = (Docente)cbDisp.SelectedValue;
            if (_docente != null)
            {
                txtNom2.Text = _docente.Nombre;
                txtDes2.Text = _docente.Descripcion;
                if (!string.IsNullOrEmpty(_docente.RutaImg))
                {
                    imgFoto2.Source = new BitmapImage(new Uri(_docente.RutaImg, UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.CargaDocentes();            
        }

        private void CargaDocentes()
        {
            DocenteBLL docentes = new DocenteBLL();
            cbDisp.ItemsSource = docentes.ObtenerAll();
        }

        private void txtDes2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.AgregaDocente(null, null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FotoBytex = ObtenerBytex();
            if (FotoBytex != null)
            {
                imgFoto2.Source = ObtenerImagen(FotoBytex);                //almacenaImagen();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FotoBytex = ObtenerBytex();
            if (FotoBytex != null)
            {
                imgFoto.Source = ObtenerImagen(FotoBytex);                //almacenaImagen();
            }
        }
        #region Tratamiento de Imagenes

        private byte[] ObtenerBytex()
        {
            System.IO.Stream stream;
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openDialog.Filter = "Pictures(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp| All Files (*.*)|*.*";
            openDialog.FilterIndex = 1;
            openDialog.Multiselect = false;
            byte[] imageData = null;

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    if ((stream = openDialog.OpenFile()) != null)
                    {
                        arcOri = openDialog.FileName;
                        ext = System.IO.Path.GetExtension(arcOri);
                        using (stream)
                        {
                            imageData = new byte[stream.Length];
                            stream.Read(imageData, 0, (int)stream.Length);
                        }
                        return imageData;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se puede subir la imagen " + ex.Message);
                    imageData = null;
                }
            }
            return imageData;
        }

        private BitmapImage ObtenerImagen(byte[] bytex)
        {
            try
            {
                BitmapImage bitImg = new BitmapImage();
                bitImg.BeginInit();
                System.IO.Stream ms = new System.IO.MemoryStream(bytex);
                bitImg.StreamSource = ms;
                bitImg.EndInit();
                return bitImg;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se puede obtener la imagen " + ex.Message);
                return null;
            }
        }

        private string almacenaImagen(string nomDoc)
        {
            try
            {
                string arcDes = System.IO.Path.Combine(rutDes, nomDoc + ext);
                if (!Directory.Exists(rutDes))
                    Directory.CreateDirectory(rutDes);
                File.Copy(arcOri, arcDes, true);
                nomImg = arcDes;
                return nomImg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            //string dirFotos = @"C:\Fotos";
            //try
            //{
            //    if (!Directory.Exists(dirFotos))
            //        Directory.CreateDirectory(dirFotos);

            //    File.Copy(rutOri, dirFotos, true);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        #endregion

    }
}
