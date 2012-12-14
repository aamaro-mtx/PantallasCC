using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace ControlHorario
{

    public partial class MainWindows : Window
    {        
        List<int> idlabs = new List<int>();
        AppInfo appInfo = new AppInfo();
        List<Uri> rutas = new List<Uri>();
        List<Aviso> lstAvisos = new List<Aviso>();
        Imagen nuevaImagen = new Imagen();
        Timer temporizador = new Timer();
        List<List<Horario>> misHorarios = new List<List<Horario>>();
        List<List<sActiva>> misSesiones = new List<List<sActiva>>();
        int index = 0, nolab, cntlab = 0, xLab = 0;
        bool isInicial = true;
        Pant Estado = Pant.AVISOS;
        enum Pant
        {
            HORARIOS,AVISOS,ALUMNOS,FIN
        }
        
        public MainWindows()
        {
            InitializeComponent();
            Timer timActu = new Timer();
            timActu.Interval = new TimeSpan(0, 5, 0);
            timActu.Tick += timActu_Tick;
        }

        private void timActu_Tick(object sender, EventArgs e)
        {
            foreach (var id in idlabs)
            {
                obtenerActual(id);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> strIDs = new List<string>();
                strIDs.Add(ConfigurationManager.AppSettings.Get("IDLab"));
                strIDs.Add(ConfigurationManager.AppSettings.Get("IDLab2"));
                strIDs.Add(ConfigurationManager.AppSettings.Get("IDLab3"));

                foreach (var i in strIDs)
                {
                    if (!string.IsNullOrEmpty(i) && i != "*")
                        idlabs.Add(int.Parse(i));
                }

                if (idlabs.Count > 0)
                {
                    foreach (var id in idlabs)
                    {
                        obtenerActual(id);
                    }                    
                    cargaRutas();
                    temporizador.Tick += temporizador_Tick;                    
                    temporizador.Interval = new TimeSpan(0, 0, 0);
                    temporizador.Start();                    
                    nolab = idlabs[cntlab];                    
                }
                else
                {
                    MessageBox.Show(this, "No se ha asignado ningun laboratorio \n verifca archivo de configuracion", appInfo.AssemblyProduct,
                                    MessageBoxButton.OK, MessageBoxImage.Information); 
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:::: ---> " + ex.Message);
                this.Close();
            }
        }

        private void temporizador_Tick(object sender, EventArgs e)
        {
            
            switch (Estado)
            {
                case Pant.HORARIOS:
                    #region Horarios

                    try
                    {
                        if (misHorarios[cntlab].Count > 0)
                        {
                            temporizador.Stop();
                            CuboHorario cbHoras = new CuboHorario(misHorarios[cntlab]);
                            cbHoras.ShowDialog();
                            mostrarTransicion();
                            temporizador.Start();
                        }
                        Estado = Pant.ALUMNOS;
                    }
                    catch (Exception)
                    {
                        Estado = Pant.ALUMNOS;
                    }
                    #endregion
                    break;

                case Pant.AVISOS:
                    #region Avisos
                    try
                    {
                        if (lstAvisos.Count > 0)
                        {
                            temporizador.Stop();
                            wndAvisos avisos = new wndAvisos(lstAvisos);
                            avisos.ShowDialog();
                            mostrarTransicion();
                            temporizador.Start();
                        }
                        Estado = Pant.HORARIOS;
                    }
                    catch (Exception)
                    {
                        Estado = Pant.HORARIOS;
                    }                    
                    //Estado = Pant.ALUMNOS;

                    #endregion
                    break;

                case Pant.ALUMNOS:
                    #region Alumnos
                    try
                    {
                        //if (misSesiones[cntSes].Count > 0)
                        //{
                            temporizador.Stop();
                            LayLaboratorio lays = new LayLaboratorio(nolab);
                            lays.ShowDialog();
                            mostrarTransicion();
                            temporizador.Start();
                        //}
                            Estado = Pant.FIN;                       
                    }
                    catch (Exception)
                    {
                        Estado = Pant.FIN;
                    }

                    #endregion
                    break;

                case Pant.FIN:
                    #region Termino de un secuencia para un laboratorio
                    
                    if (cntlab >= xLab)
                    {
                        cntlab = 0;
                        isInicial = true;
                        misHorarios.Clear();
                        misSesiones.Clear();
                        //obtenerActual(nolab);
                    }
                    else
                        cntlab++;

                    nolab = idlabs[cntlab];               
                    index = 0;
                    Estado = Pant.AVISOS;
                    obtenerActual(nolab);
                    #endregion
                    break;
            }
        }

        private void mostrarTransicion()
        {
            //MessageBox.Show(index.ToString() + " " + rutas[index]);
            CuboWin cbTran = new CuboWin(rutas[index]);
            cbTran.ShowDialog();
            if (index > 3)
                index = 0;
            index++;
        }

        private void obtenerActual(int idlab)
        {
            List<Horario> lstHoras = new List<Horario>();            
                       
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
            #endregion

            lstAvisos = avisos.ObtenerAll();
            misSesiones.Add(alumnos.GetByLab(idlab));

            string diaSn = string.Empty;
            string diaActual = DateTime.Now.DayOfWeek.ToString();
            string horaActual = DateTime.Now.Hour.ToString() + ":00";        

            try
            {
                int hraSer = int.Parse(horaActual.Replace(":00", ""));
                dias.TryGetValue(diaActual.ToUpper(), out diaSn);

                // OPTIMIZACION
                var HrasDelDia = horarios.ObtenerByLab(idlab)
                    .Where(t => t.Dia == diaSn)  
                    .OrderBy(o=> o.HoraInicio)
                    .ToList();
              
                //
                var hraAct = HrasDelDia
                  .Where(t => t.HoraInicio == horaActual ||
                      int.Parse(t.HoraFin.Replace(":00", "")) > hraSer)
                  .FirstOrDefault();
                var hrsPos = HrasDelDia
                    .Where(t => int.Parse(t.HoraInicio.Replace(":00", "")) > hraSer)
                    .Take(3).ToList();

                //var hraAct = horarios.ObtenerByLab(idlab)
                //    .Where(t => t.Dia == diaSn && t.HoraInicio == horaActual)
                //    .FirstOrDefault();
                //var hrsPos = horarios.ObtenerByLab(idlab)
                //    .Where(t => t.Dia == diaSn && int.Parse(t.HoraInicio.Replace(":00", "")) > int.Parse(tmpA))
                //    .Take(3).ToList();

                if (hraAct != null) lstHoras.Add(hraAct);
                lstHoras.AddRange(hrsPos);
                misHorarios.Add(lstHoras);
                if (isInicial)
                {
                    isInicial = false;
                    xLab = misHorarios.Count ;
                }
            }
            catch (Exception)
            {
                lstHoras.Clear();
                //MessageBox.Show("Error >.<!! " + ex.Message);
            }
        }

        private void cargaRutas()
        {
            string r = AppDomain.CurrentDomain.BaseDirectory;
            rutas.Add(new Uri(r + @"Imagenes\Mision.JPG", UriKind.RelativeOrAbsolute));
            rutas.Add(new Uri(r + @"Imagenes\Politica.JPG", UriKind.RelativeOrAbsolute));
            rutas.Add(new Uri(r + @"Imagenes\Vision.JPG", UriKind.RelativeOrAbsolute));
            rutas.Add(new Uri(r + @"Imagenes\Equidad.JPG", UriKind.RelativeOrAbsolute));
        }
    }
}
