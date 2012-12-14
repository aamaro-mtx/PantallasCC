
using Entidades;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ControlHorario
{

    public class Dibujar
    {
        public void dibujaLaboratorio(int id,Grid grid)
        {
            CentroComputo centro = CentroComputoBLL.LoadFromXML();
            var lab = centro.Laboratorios.Where(l => l.Numero == id).FirstOrDefault();
            dibujaEquipos(lab, grid);
        }

        public  void DibujaLayout(TabControl con)
        {
            CentroComputo centro = CentroComputoBLL.LoadFromXML();

            foreach (laboratorio lab in centro.Laboratorios)
            {
                TabItem tabItem = dibujaEquipos(lab);
                con.Items.Add(tabItem);
            }
        }

        private void dibujaEquipos(laboratorio lab, Grid grid)
        {
            //Grid grid = new Grid();
            TabItem tabItem = new TabItem();
            List<uctComputadora> compus = new List<uctComputadora>();
            //grid.Children.Add(AgregarPizarron());
            AlumnoBLL alumnos = new AlumnoBLL();
            var sesiones = alumnos.getActivas(lab.Numero);
            for (int j = 0; j < lab.Equipos.Count; j++)
            {
                compus.Add(new uctComputadora(lab.Numero, lab.Equipos[j].Numero));
                compus[j].Margin = new Thickness(lab.Equipos[j].X - 100, lab.Equipos[j].Y, 0.0, 0.0);
                compus[j].MouseDown += new MouseButtonEventHandler(Compu_MouseDown);
                //grid.Children.Add(compus[j]);
            }

            foreach (var se in sesiones)
            {
                compus[se.SE_NUM_EQP].EstadoEquipo = uctComputadora.Estado.Ocupada;
                compus[se.SE_NUM_EQP].Usuario = se.AS_US_DESCR;
            }
            for (int i = 0; i < compus.Count; i++)
                grid.Children.Add(compus[i]);
        }

        private TabItem dibujaEquipos(laboratorio lab)
        {
            Grid grid = new Grid();
            TabItem tabItem = new TabItem();
            List<uctComputadora> compus = new List<uctComputadora>();
            //grid.Children.Add(AgregarPizarron());
            AlumnoBLL alumnos = new AlumnoBLL();
            var sesiones = alumnos.getActivas(lab.Numero);
            for (int j = 0; j < lab.Equipos.Count; j++)
            {
                compus.Add(new uctComputadora(lab.Numero, lab.Equipos[j].Numero));              
                compus[j].Margin = new Thickness(lab.Equipos[j].X - 100, lab.Equipos[j].Y, 0.0, 0.0);
                compus[j].MouseDown += new MouseButtonEventHandler(Compu_MouseDown);                
                //grid.Children.Add(compus[j]);
            }

            foreach (var se in sesiones)
            {
                compus[se.SE_NUM_EQP].EstadoEquipo = uctComputadora.Estado.Ocupada;
                compus[se.SE_NUM_EQP].Usuario = se.AS_US_DESCR;
            }
            for (int i = 0; i < compus.Count; i++)
                grid.Children.Add(compus[i]);
    
            tabItem.Content = grid;
            tabItem.Header = string.Format("Laboratorio {0}", lab.Numero);

            return tabItem;
        }

        private void Compu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("No implementada");
        }
    }
}