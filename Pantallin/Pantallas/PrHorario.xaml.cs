//using Cubo;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Screen = System.Windows.Forms.Screen;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace ControlHorario
{
    public partial class PrHorario : Window
    {
        public PrHorario()
        {
            InitializeComponent();
            izq = arr = 50;  
        }

        //List<Imagen> listaImagenes = new List<Imagen>();
        //List<Horario> lstDobleHoras = new List<Horario>();
        //List<Uri> rutas = new List<Uri>();
        //List<Horario> lstHoras = new List<Horario>();        
        //List<Aviso> lstAvisos = new List<Aviso>();
        //List<SesionActual> lstUsuarios = new List<SesionActual>();
        List<Uri> rutas = new List<Uri>();
        List<Horario> lstHoras = null;
        List<Aviso> lstAvisos = null;
        List<SesionActual> lstUsuarios = null;
        Imagen nuevaImagen = new Imagen();
        Timer timHoras = new Timer();
      
        int izq, arr, paso = 2;
        int idLab,idlab2, index = 0,panActual = 0;




        #region Version Soporta nLabs       
              
        #endregion

        #region Metodos Generales



        private void cargaRutas()
        {
            string r = AppDomain.CurrentDomain.BaseDirectory;
            rutas.Add(new Uri(r + @"Imagenes\Mision.JPG", UriKind.Relative));
            rutas.Add(new Uri(r + @"Imagenes\Politica.JPG", UriKind.Relative));
            rutas.Add(new Uri(r + @"Imagenes\Vision.JPG", UriKind.Relative));
            rutas.Add(new Uri(r + @"Imagenes\Equidad.JPG", UriKind.Relative));
        }

        private void MuestraCubo(Uri ruta)
        {
            CuboWin cubo = new CuboWin(ruta);
            cubo.ShowDialog();
        }

        private void MuestraCuboHoras()
        {
            CuboHorario cubo = new CuboHorario(lstHoras);
            cubo.ShowDialog();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                timHoras.Interval = new TimeSpan(0, 0, 10);
                switch (paso)
                {
                    case 1:
                        ActualizaPantalla(lstHoras);
                        break;
                    case 2:
                        ActualizaPantalla(lstAvisos);
                        break;
                    case 3:
                        ActualizaPantalla(lstUsuarios);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la aplicacion" + ex.Message);
            }
        }

        private void ActualizaPantalla<T>(List<T> lista)
        {
            if (panActual >= lista.Count)
            {
                lista = null;
                puntoFinal();
            }
            else
            {
                if (paso == 1)
                {
                    MuestraCuboHoras();                                    
                    lstHoras = null;                    
                    puntoFinal();
                }
                else if (paso == 3)
                {
                    MuestraLayout();                    
                    lstUsuarios = null;
                    puntoFinal();
                }
                else
                {
                    this.AgregarControl(lista[panActual]);
                    panActual++;
                }
            }
        }

        private void MuestraLayout()
        {
            LayLaboratorio lay = new LayLaboratorio(idLab);              
            lay.ShowDialog();           
        }

        private void puntoFinal()
        {            
            panActual = 0;             
            timHoras.Interval = new TimeSpan(0, 0, 0);
            arr = izq = 50;
            if (index > 3)
                index = 0;
            MuestraCubo(rutas[index]);
            paso++;
            index++;
            if (paso > 3)
            {
                paso = 1;
                Inicia_Secuencia(null, null);
            }
        }

        private void AgregarControl<T>(T Actual)
        {
            try
            {
                #region Inicializacion
                Aviso avsActual = Actual as Aviso;              
                var pan = Screen.PrimaryScreen;
                int anc = pan.WorkingArea.Width - 200;
                int lar = pan.WorkingArea.Height - 100;
                int x = (pan.WorkingArea.Width - anc) / 2;
                int y = (pan.WorkingArea.Height - lar) / 2;                
                #endregion   
           
                if (avsActual != null)
                {
                    SolidColorBrush sbr = new SolidColorBrush(Color.FromRgb(1, 53, 74));
                    nuevaImagen.Dispose();
                    using (nuevaImagen = new Imagen())
                    {
                        #region Inicializador
                        nuevaImagen.Width = anc;
                        nuevaImagen.Height = lar;
                        nuevaImagen.lblNoticia.Text = avsActual.Noticia;
                        nuevaImagen.lblNoticia.Foreground = sbr;
                        nuevaImagen.lblNoticia.FontSize = 35;
                        nuevaImagen.lblCarrera.Visibility = Visibility.Hidden;
                        nuevaImagen.lblGrupo.Visibility = Visibility.Hidden;
                        nuevaImagen.lblMateria.Visibility = Visibility.Hidden;
                        nuevaImagen.lblDocente.Visibility = Visibility.Hidden;
                        nuevaImagen.lblHorario.Visibility = Visibility.Hidden;
                        ImageBrush imgBr = new ImageBrush();
                        string r = AppDomain.CurrentDomain.BaseDirectory;
                        imgBr.ImageSource = (new BitmapImage
                            (new Uri(r + @"Imagenes\AvisosCC.png", UriKind.Relative)));
                        nuevaImagen.rectImagen.Fill = imgBr;
                        #endregion
                        #region Acom odar
                        //AdministradorSuperficie.ImagenActiva.Dispose();
                        using (AdministradorSuperficie.ImagenActiva)
                        {
                            AdministradorSuperficie.ImagenActiva = nuevaImagen;
                            LayoutRoot.Children.Add(AdministradorSuperficie.ImagenActiva);

                            Canvas.SetLeft(AdministradorSuperficie.ImagenActiva, x);
                            Canvas.SetTop(AdministradorSuperficie.ImagenActiva, y);

                            Random rnd = new Random((int)DateTime.Now.Ticks);
                            double aleatorio = rnd.NextDouble();
                            if (aleatorio >= 0.5)
                                AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * 10);
                            else
                                AdministradorSuperficie.ImagenActiva.Rotar(aleatorio * -10);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la aplicacion" + ex.Message);
            }
        }
       
        #endregion

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                cargaRutas();
                lblTest.Content += "Iniciando...\n";
                string llave = ConfigurationManager.AppSettings.Get("IDLab");
                string llave2 = ConfigurationManager.AppSettings.Get("IDLab2");

                if (int.TryParse(llave, out idLab))
                {
                    if (!string.IsNullOrEmpty(llave2))
                    {
                        int.TryParse(llave2, out idlab2);
                    }
                    timHoras.Tick += new EventHandler(TimerTick);
                    this.obtenerActual();
                    timHoras.Interval = new TimeSpan(0, 0, 0);
                    timHoras.Start();
                }
                else
                {
                    MessageBox.Show("..:: ERROR :::..\nLa aplicacion se cerrara verifique archivo de configuracion");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:::: ---> " + ex.Message);
                this.Close();
            }
        }

        private void Inicia_Secuencia(object sender, RoutedEventArgs e)
        {
            this.obtenerActual();
            lblTest.Content += "Iniciadno secuencia de nuevo";
            if (index == 3)
            {
                timHoras.Interval = new TimeSpan(0, 0, 0);
                lblTest.Content += "Iniciadno secuencia de index ===3";
            }
            else
            {
                timHoras.Interval = new TimeSpan(0, 0, 10);
                lblTest.Content += "Iniciadno secuencia de nuevo index ==5";
            }
        }

        private void obtenerActual()
        {
            lstHoras = new List<Horario>();
            lstAvisos = new List<Aviso>();
            lstUsuarios = new List<SesionActual>();
            #region Inicializacion
            Dictionary<string, string> dias = new Dictionary<string, string>();
            dias.Add("MONDAY", "LUNES");
            dias.Add("TUESDAY", "MARTES");
            dias.Add("WEDNESDAY", "MIERCOLES");
            dias.Add("THURSDAY", "JUEVES");
            dias.Add("FRIDAY", "VIERNES");
            dias.Add("SATURDAY", "SABADO");
            dias.Add("SUNDAY", "DOMINGO");
                        
            HorarioBLL horarios = new HorarioBLL();
            AvisoBLL avisos = new AvisoBLL();
            AlumnoBLL alumnos = new AlumnoBLL();
            lstAvisos = avisos.ObtenerAll();
            lstUsuarios = alumnos.ObtenerTodos();
            string diaSn = string.Empty;
            string diaActual = DateTime.Now.DayOfWeek.ToString();            
            string horaActual = DateTime.Now.Hour.ToString() + ":00";

            #endregion

            try
            {
                string tmpA = horaActual.Replace(":00", "");
                dias.TryGetValue(diaActual.ToUpper(), out diaSn);
                var hraAct = horarios.ObtenerByLab(idLab)
                    .Where(t => t.Dia == diaSn && t.HoraInicio == horaActual).FirstOrDefault();
                var hrsPos = horarios.ObtenerByLab(idLab)
                    .Where(t => t.Dia == diaSn && int.Parse(t.HoraInicio.Replace(":00", "")) > int.Parse(tmpA)).Take(3).ToList();

                if (hraAct != null) lstHoras.Add(hraAct);
                lstHoras.AddRange(hrsPos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error >.<!! " + ex.Message); 
            }
        }
    }
}
