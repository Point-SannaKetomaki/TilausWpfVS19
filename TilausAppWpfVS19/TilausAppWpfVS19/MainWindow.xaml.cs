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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TilausAppWpfVS19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HaeAsiakkaat();  //Täytetään Asiakas-combobox
        }


        private void HaeAsiakkaat()
        {
            List<cbPairAsiakas> cbPairAsiakkaat = new List<cbPairAsiakas>();
            TilausDBEntities entities = new TilausDBEntities();

            var asiakkaat = from a in entities.Asiakkaat
                            select a;

            foreach (var asiakas in asiakkaat)
            {
                cbPairAsiakkaat.Add(new cbPairAsiakas(asiakas.Nimi, asiakas.AsiakasID));
            }

            //Combo-box:n nimi on cbAsiakkaat
            cbAsiakkaat.DisplayMemberPath = "asiakasNimi";  //listalla näkyy nimi, tulee cbPaisAsiakas.cs luokasta
            cbAsiakkaat.SelectedValuePath = "asiakasNumero"; //tk:aan tallennettaessa käytetään as-numeroa, cbPairAsiakas.cs luokasta
            cbAsiakkaat.ItemsSource = cbPairAsiakkaat;
        }

        private void CbAsiakkaat_DropDownClosed(object sender, EventArgs e)
        {
            cbPairAsiakas cbp = (cbPairAsiakas)cbAsiakkaat.SelectedItem;  //mikä asiakas valittiin listalta?
            string AsiakasNimi = cbp.asiakasNimi;
            int AsiakasNumero = cbp.asiakasNumero;
            txtAsiakasNumero.Text = "";
            txtAsiakasNumero.Text = AsiakasNumero.ToString(); //tulostetaan valitun asiakkaan as.numero
        }
    }
}
