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

        DateTime tänään = DateTime.Today;
        decimal RivienSummaYht = 0;

        public MainWindow()
        {
            InitializeComponent();
            HaeAsiakkaat();  //Täytetään Asiakas-combobox
            HaeTuotteet(); //Täytetään Tuote-combobox
            HaePostinumerot(); //Täytetään Postinumerot-combobox

            
            dpTilausPaiva.SelectedDate = tänään;  //Datepickerin oletuspvm
            dpToimitusPaiva.SelectedDate = tänään.AddDays(14);  //Datepickerin oletuspvm
            
            //Luodaan ikäänkuin ilmaan DataGridTextColumn -tyyppisiä olioita
            DataGridTextColumn colTilausRiviNumero = new DataGridTextColumn();
            DataGridTextColumn colTilausNumero = new DataGridTextColumn();
            DataGridTextColumn colTuoteNumero = new DataGridTextColumn();
            DataGridTextColumn colTuoteNimi = new DataGridTextColumn();
            DataGridTextColumn colMaara = new DataGridTextColumn();
            DataGridTextColumn colAHinta = new DataGridTextColumn();
            DataGridTextColumn colRivinSumma = new DataGridTextColumn();
            
            //Oliot sidotaan tietyn nimiseen sarakkeeseen < --kohdistuu myöhemmin olion ominaisuuksiin, kunhan olio on ensin viety listalle
            //PUNAISET tässä alla ovat TilausRivi.cs:n ominaisuuksien nimet
            colTilausRiviNumero.Binding = new Binding("TilausRiviNumero");
            colTilausNumero.Binding = new Binding("TilausNumero");
            colTuoteNumero.Binding = new Binding("TuoteNumero");
            colTuoteNimi.Binding = new Binding("TuoteNimi");
            colMaara.Binding = new Binding("Maara");
            colAHinta.Binding = new Binding("AHinta");
            colRivinSumma.Binding = new Binding("Summa");
            
            //DataGridin otsikot 
            colTilausRiviNumero.Header = "Tilausrivin numero";
            colTilausNumero.Header = "Tilauksen numero";
            colTuoteNumero.Header = "Tuotenumero";
            colTuoteNimi.Header = "Tuotenimi";
            colMaara.Header = "Määrä";
            colAHinta.Header = "A-Hinta";
            colRivinSumma.Header = "Rivin summa";
            
            //Edellä "ilmaan" luotujen sarakkeiden sijoitus konkreettiseen DataGridiin, joka on luotu formille
            dgTilausrivit.Columns.Add(colTilausRiviNumero);
            dgTilausrivit.Columns.Add(colTilausNumero);
            dgTilausrivit.Columns.Add(colTuoteNumero);
            dgTilausrivit.Columns.Add(colTuoteNimi);
            dgTilausrivit.Columns.Add(colMaara);
            dgTilausrivit.Columns.Add(colAHinta);
            dgTilausrivit.Columns.Add(colRivinSumma);
        }

        
        private void HaeAsiakkaat()
        {
            List<cbPairAsiakas> cbPairAsiakkaat = new List<cbPairAsiakas>();  //luodaan uusi cbPairAsiakas-tyyppinen lista
            TilausDBEntities entities = new TilausDBEntities();  //alustetaan ja avataan tk-yhteys

            var asiakkaat = from a in entities.Asiakkaat        //haetaan kaikki tiedot kanasta asiakkaat-muuttujaan
                            select a;

            foreach (var asiakas in asiakkaat)                  //käydään läpi jokainen kannasta tullut rivi ja lisätään sen cbPairAsiakkaat-nimiseen listaan
            {
                cbPairAsiakkaat.Add(new cbPairAsiakas(asiakas.Nimi, asiakas.AsiakasID));
            }

            //Combo-box:n nimi on cbAsiakkaat
            cbAsiakkaat.DisplayMemberPath = "asiakasNimi";  //listalla cbAsiakkaat näkyy nimi, tulee cbPairAsiakas.cs luokasta
            cbAsiakkaat.SelectedValuePath = "asiakasNumero"; //tk:aan tallennettaessa käytetään as-numeroa, cbPairAsiakas.cs luokasta
            cbAsiakkaat.ItemsSource = cbPairAsiakkaat;  //comboboxin tiedot tulevat cbPairAsiakkaat -listalta
        }


        

        private void CbAsiakkaat_DropDownClosed(object sender, EventArgs e)
        {
            cbPairAsiakas cbp = (cbPairAsiakas)cbAsiakkaat.SelectedItem;  //mikä asiakas valittiin listalta?
            string AsiakasNimi = cbp.asiakasNimi;
            int AsiakasNumero = cbp.asiakasNumero;
            txtAsiakasNumero.Text = "";
            txtAsiakasNumero.Text = AsiakasNumero.ToString(); //tulostetaan valitun asiakkaan as.numero
        }

        

        private void BtnLuoTilaus_Click(object sender, RoutedEventArgs e)
        {
            TilausOtsikko uusiTilaus = new TilausOtsikko
            {
                //luodaan uusi olio: uusiTilaus
                AsiakasNumero = int.Parse(txtAsiakasNumero.Text),
                ToimitusOsoite = txtToimitusOsoite.Text,
                Postinumero = cbPostinumerot.Text,
                TilausPvm = dpTilausPaiva.SelectedDate.Value,
                ToimitusPvm = dpToimitusPaiva.SelectedDate.Value
            };

            //TÄSSÄ PITEMPI VERSIO TUOSTA EDELLISESTÄ
            //TilausOtsikko uusiTilaus = new TilausOtsikko();  //luodaan uusi olio: uusiTilaus
            //uusiTilaus.AsiakasNumero = int.Parse(txtAsiakasNumero.Text);
            //uusiTilaus.ToimitusOsoite = txtToimitusOsoite.Text;
            //uusiTilaus.Postinumero = txtPostiNumero.Text;
            //uusiTilaus.TilausPvm = dpTilausPaiva.SelectedDate.Value;
            //uusiTilaus.ToimitusPvm = dpToimitusPaiva.SelectedDate.Value;
            
            txtToimitusAika.Text = uusiTilaus.LaskeToimitusAika();

            txtTilausNumero.Text = VieTilausKantaan(uusiTilaus); //tietojen vienti kantaan EF:n avulla

            //TODO: Tallennetun tilauksen yhteenveto ruudulle
        }
        //

        private string VieTilausKantaan(TilausOtsikko uusiTilaus)
        {
            try
            {
                TilausDBEntities entities = new TilausDBEntities();
                Tilaukset dbItem = new Tilaukset()  // uusi rivi Tilaukset-tauluun 
                {
                    AsiakasID = uusiTilaus.AsiakasNumero,
                    Toimitusosoite = uusiTilaus.ToimitusOsoite,
                    Postinumero = uusiTilaus.Postinumero,
                    Tilauspvm = uusiTilaus.TilausPvm,
                    Toimituspvm = uusiTilaus.ToimitusPvm
                };

                entities.Tilaukset.Add(dbItem); //viedään tiedot kantaan
                entities.SaveChanges(); //tallennetaan muutokset

                int id = dbItem.TilausID; //haetaan juuri tallennetun tilauksen ID (=PK)
                return id.ToString(); //palautetaan em. id käyttöliittymään merkiksi onnistuneesta tallennuksesta
            }
            catch (Exception)
            {
                return "0";
            }
        }

        private void HaeTuotteet()
        {
            List<cbPairTuote> cbPairTuotteet = new List<cbPairTuote>();
            TilausDBEntities entities = new TilausDBEntities();

            var tuotteet = from t in entities.Tuotteet
                           select t;

            foreach (var tuote in tuotteet)
            {
                cbPairTuotteet.Add(new cbPairTuote(tuote.Nimi, tuote.TuoteID));
            }

            //Combo-box:n nimi on cbTuoteTiedot
            cbTuoteTiedot.DisplayMemberPath = "tuoteNimi";
            cbTuoteTiedot.SelectedValuePath = "tuoteNumero";
            cbTuoteTiedot.ItemsSource = cbPairTuotteet;
        }

        private void CbTuoteTiedot_DropDownClosed(object sender, EventArgs e)
        {
            cbPairTuote cbp = (cbPairTuote)cbTuoteTiedot.SelectedItem;  //mikä tuote valittiin listalta?
            string TuoteNimi = cbp.tuoteNimi;
            int TuoteNumero = cbp.tuoteNumero;
            txtTuotekoodi.Text = TuoteNumero.ToString(); //tulostetaan valitun tuotteen tuotenumero
        }

        private void BtnLisaaRivi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //tulostetaan rivi gridiin

                TilausRivi tilausRivi = new TilausRivi();
                tilausRivi.TilausNumero = int.Parse(txtTilausNumero.Text);
                tilausRivi.TuoteNumero = int.Parse(txtTuotekoodi.Text);
                tilausRivi.TuoteNimi = cbTuoteTiedot.Text;
                tilausRivi.Maara = int.Parse(txtMaara.Text);
                tilausRivi.AHinta = Convert.ToDecimal(txtHinta.Text);

                tilausRivi.TilausRiviNumero = VieTilausRiviKantaan(tilausRivi);


                RivienSummaYht += tilausRivi.RiviSumma(); //Kuten tämä: RivinSummaYht = RivinSummaYht + TilausR.RiviSumma();
                txtRivienSumma.Text = RivienSummaYht.ToString();
                dgTilausrivit.Items.Add(tilausRivi);

                cbTuoteTiedot.Text = "";
                txtTuotekoodi.Text = "";
                txtMaara.Text = "";
                txtHinta.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int VieTilausRiviKantaan(TilausRivi TilausR)
        {
            TilausDBEntities db = new TilausDBEntities();

            Tilausrivit dbItem = new Tilausrivit()   //nimi voisi olla esim. uusiTilausrivi eikä dbItem
            {
                TilausID = TilausR.TilausNumero,
                TuoteID = TilausR.TuoteNumero,
                //Tuotenimi = TilausR.TuoteNimi,   Tuotenimeä ei viedä tietokantaan, koska siellä ei ole ko. saraketta !!!!!
                Maara = TilausR.Maara,
                Ahinta = TilausR.AHinta
            };

            db.Tilausrivit.Add(dbItem);
            db.SaveChanges();

            int id = dbItem.TilausriviID;
            return id;
        }

        private void HaePostinumerot()
        {
            List<cbPairPostinumero> cbPairPostinumerot = new List<cbPairPostinumero>();
            TilausDBEntities entities = new TilausDBEntities();

            var postinumerot = from p in entities.Postitoimipaikat
                               select p;                            //haetaan tiedot kannasta listalle

            foreach (var pnro in postinumerot)
            {
                cbPairPostinumerot.Add(new cbPairPostinumero(pnro.Postinumero));
            }


            //Combo-box:n nimi on cbPostinumerot
            cbPostinumerot.DisplayMemberPath = "postiNumero";
            cbPostinumerot.ItemsSource = cbPairPostinumerot;
        }

        private void BtnPostitoimipaikat_Click(object sender, RoutedEventArgs e)
        {
            formPostitoimipaikat ptmip = new formPostitoimipaikat();
            ptmip.Show();
        }

        private void BtnTuotetiedot_Click(object sender, RoutedEventArgs e)
        {
            formTuotetiedot tuotetiedot = new formTuotetiedot();
            tuotetiedot.Show();
        }
    }
}
