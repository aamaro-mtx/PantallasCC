using Entidades;
using System.Linq;
using Negocio;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using PUsuarios;

namespace HorarioS
{
    public partial class VHorario : Window
    {
        public VHorario()
        {
            InitializeComponent();            
        }
        AppInfo appInfo = new AppInfo();

        private void inicializarCtrls()
        {
            ctrHo_lun.Dia = "LUNES"; ctrHo_Vie.Dia = "VIERNES"; 
            ctrHo_Mar.Dia = "MARTES"; ctrHo_Sab.Dia = "SABADO";
            ctrHo_Mie.Dia = "MIERCOLES"; ctrHo_Dom.Dia = "DOMINGO";
            ctrHo_Jue.Dia = "JUEVES";
        }

        private void PruebBoton(object sender, RoutedEventArgs e)
        {
            
            HorarioBLL _horarios = new HorarioBLL();
            List<HorarioC> lst = new List<HorarioC>()
            {
                ctrHo_lun,ctrHo_Mar,ctrHo_Mie,ctrHo_Jue,ctrHo_Vie,ctrHo_Sab,ctrHo_Dom
            };
            try
            {
                int idLab = ((Laboratorio)cbLab.SelectedValue).ID;
                int idDoc = ((Docente)cbDoc.SelectedValue).ID;
                int idAsi = ((Asignatura)cbMat.SelectedValue).ID;
                int idGrp = ((Grupo)cbGrp.SelectedValue).ID;

                foreach (var ctr in lst)
                {
                    if (ctr.Checado)
                    {
                        if (llenos(ctr.HrInicio, ctr.HrFin))
                        {
                            Horario _hora = new Horario()
                            {
                                IDAsignatura = idAsi,
                                Dia = ctr.Dia,
                                HoraInicio = ctr.HrInicio,
                                HoraFin = ctr.HrFin,
                                IDLab = idLab,
                                IDDocente = idDoc,
                                IDGrupo = idGrp
                            };
                            if (_horarios.Agregar(_hora) != -1)
                            {
                                MessageBox.Show(this, string.Format("Dia {0} Agregado correctamente", ctr.Dia), appInfo.AssemblyProduct,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show(this, "Algo ha salido mal :(", appInfo.AssemblyProduct,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "No debe de tener campos vacios", appInfo.AssemblyProduct,
                               MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }                    
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show(this,"Por favor seleccione un Horario ",appInfo.AssemblyProduct,
                        MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(this,"Error general ", appInfo.AssemblyProduct,
                        MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private bool llenos(string p1, string p2)
        {
            return (!string.IsNullOrEmpty(p1) && !string.IsNullOrEmpty(p2));
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LaboratorioBLL labs = new LaboratorioBLL();
            DocenteBLL docentes = new DocenteBLL();
            AsignaturaBLL asignaturas = new AsignaturaBLL();
            GrupoBLL grupos = new GrupoBLL();

            cbLab.ItemsSource = labs.ObtenerAll().OrderBy(s => s.ID);
            cbDoc.ItemsSource = docentes.ObtenerAll().OrderBy(s => s.Nombre);
            cbMat.ItemsSource = asignaturas.ObtenerAll().OrderBy(s => s.Nombre);
            cbGrp.ItemsSource = grupos.ObtenerAll().OrderBy(s=>s.Nombre);
            this.inicializarCtrls();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int idLab = ((Laboratorio)cbLab.SelectedValue).ID;
                int idDoc = ((Docente)cbDoc.SelectedValue).ID;
                int idAsi = ((Asignatura)cbMat.SelectedValue).ID;
                int idGrp = ((Grupo)cbGrp.SelectedValue).ID;
            }
            catch ( Exception ex)
            {
                MessageBox.Show("ERROr : " + ex.Message);
            }
        }

    }
}
