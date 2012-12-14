using System.Windows;
using System.Windows.Controls;


namespace HorarioS
{
    public partial class HorarioC : UserControl
    {
        public string HrInicio { get; set; }
        public string HrFin { get; set; }
        public bool Checado { get; set; }
        public string Dia { get; set; }

        public HorarioC()
        {
            InitializeComponent();
        }

        private void CargaItems(object sender, RoutedEventArgs e)
        {
            for (int i = 7; i <= 9; i++)
            {
                cbHIni.Items.Add("0" + i + ":00");
                cbHFin.Items.Add("0" + i + ":00");
            }
            for (int i = 10; i <= 21; i++)
            {
                cbHIni.Items.Add(i + ":00");
                cbHFin.Items.Add(i + ":00");
            }
        }

        //private void CreaHorario(object sender, RoutedEventArgs e)
        //{
        //    if (!llenos(cbHIni.Text) && !llenos(cbHFin.Text))
        //    {
        //        HrInicio = cbHIni.Text;
        //        hrFin = cbHFin.Text;
        //        checado = chkDia.IsChecked;
        //    }
        //    else
        //        MessageBox.Show("Ingrese los dos horarios");           
        //}
        private bool llenos(string cade)
        {
            return string.IsNullOrEmpty(cade);
        }

        private void chkDia_Checked(object sender, RoutedEventArgs e)
        {
            Checado = true;
        }

        private void cbHIni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HrInicio = (string)cbHIni.SelectedValue;
        }

        private void cbHFin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HrFin = (string)cbHFin.SelectedValue;
        }

        private void chkDia_Unchecked(object sender, RoutedEventArgs e)
        {
            Checado = false;
        }
    }
}
