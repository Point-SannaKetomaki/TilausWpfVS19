using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TilausAppWpfVS19
{
    /// <summary>
    /// Interaction logic for formTuotetiedot.xaml
    /// </summary>
    public partial class formTuotetiedot : Window
    {
        TilausDBEntities dBEntities = new TilausDBEntities();

        public formTuotetiedot()
        {
            InitializeComponent();
            HaeTuotetiedot();
        }

        private void HaeTuotetiedot()
        {
            List<Tuotteet> tuotteets = new List<Tuotteet>();

            var tuotetiedot = from d in dBEntities.Tuotteet
                              select d;

            dgTuotetiedotLista.ItemsSource = tuotetiedot.ToList();
        }


        private void BtnLisaaUusiTuote_Click(object sender, RoutedEventArgs e)
        {
            Tuotteet uusiTuote = new Tuotteet();

            uusiTuote.Nimi = txtTuoteNimi.Text;
            uusiTuote.Ahinta = decimal.Parse(txtTuoteHinta.Text);

            dBEntities.Tuotteet.Add(uusiTuote);
            dBEntities.SaveChanges();

            HaeTuotetiedot();
            txtTuoteNimi.Text = "";
            txtTuoteHinta.Text = "";
        }

        private void DgTuotetiedotLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTuotetiedotLista.SelectedIndex >= 0)
            {
                //haetaan valitun rivin ensimmäisen sarakkeen sisältö
                TextBlock TuoteID = dgTuotetiedotLista.Columns[0].GetCellContent(
                    dgTuotetiedotLista.Items[dgTuotetiedotLista.SelectedIndex]) as TextBlock;
                if (TuoteID != null)
                {
                    txtTuoteID.Text = TuoteID.Text;
                }

                //haetaan valitun rivin toisen sarakkeen sisältö
                TextBlock Tuotenimi = dgTuotetiedotLista.Columns[1].GetCellContent(
                    dgTuotetiedotLista.Items[dgTuotetiedotLista.SelectedIndex]) as TextBlock;
                if (Tuotenimi != null)
                {
                    txtTuoteNimiUpdDel.Text = Tuotenimi.Text;
                }

                //haetaan valitun rivin kolmannen sarakkeen sisältö
                TextBlock TuoteHinta = dgTuotetiedotLista.Columns[2].GetCellContent(
                    dgTuotetiedotLista.Items[dgTuotetiedotLista.SelectedIndex]) as TextBlock;
                if (TuoteHinta != null)
                {
                    txtTuoteHintaUpdDel.Text = TuoteHinta.Text;
                }
            }
        }

        private void BtnPäivitäTuotetiedot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPoistaTuotetiedot_Click(object sender, RoutedEventArgs e)
        {
            int tuoteid = int.Parse(txtTuoteID.Text);

            Tuotteet poistaTuote = dBEntities.Tuotteet.Find(tuoteid);

            if (poistaTuote !=null)
            {
                dBEntities.Tuotteet.Remove(poistaTuote);
            }

            dBEntities.SaveChanges();

            HaeTuotetiedot();
            txtTuoteID.Text = "";
            txtTuoteNimiUpdDel.Text = "";
            txtTuoteHintaUpdDel.Text = "";
        }

        private void BtnSuljeTuotetiedot_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
