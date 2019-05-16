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
    /// Interaction logic for formPostitoimipaikat.xaml
    /// </summary>
    public partial class formPostitoimipaikat : Window
    {
        TilausDBEntities entities = new TilausDBEntities();

        public formPostitoimipaikat()
        {
            InitializeComponent();
            HaePostitoimipaikat();
        }

        private void HaePostitoimipaikat()
        {
            List<Postitoimipaikat> lstPosTmp = new List<Postitoimipaikat>();
            

            var postmpt = from posnot in entities.Postitoimipaikat
                          select posnot;

            dgPostitoimipaikat.ItemsSource = postmpt.ToList();
        }

        private void BtnLisaaPtmip_Click(object sender, RoutedEventArgs e)
        {
            Postitoimipaikat post = new Postitoimipaikat();
            post.Postinumero = txtPosNro.Text;
            post.Postitoimipaikka = txtPosTmip.Text;
            entities.Postitoimipaikat.Add(post);
            entities.SaveChanges();

            HaePostitoimipaikat();
            txtPosNro.Text = "";
            txtPosTmip.Text = "";
        }

        

        private void DgPostitoimipaikat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPostitoimipaikat.SelectedIndex >=0)
            {
                //haetaan valitun rivin ensimmäisen sarakkeen sisältö
                TextBlock Postinumero = dgPostitoimipaikat.Columns[0].GetCellContent(
                    dgPostitoimipaikat.Items[dgPostitoimipaikat.SelectedIndex]) as TextBlock;
                if (Postinumero != null)
                {
                    txtPosNroUpdDel.Text = Postinumero.Text;
                }

                //haetaan valitun rivin toisen sarakkeen sisältö
                TextBlock Postitoimipaikka = dgPostitoimipaikat.Columns[1].GetCellContent(
                    dgPostitoimipaikat.Items[dgPostitoimipaikat.SelectedIndex]) as TextBlock;
                if (Postitoimipaikka != null)
                {
                    txtPosTmipUpdDel.Text = Postitoimipaikka.Text;
                }
            }
            
        }

        

        private void BtnPoistaPTmip_Click(object sender, RoutedEventArgs e)
        {
            Postitoimipaikat post = entities.Postitoimipaikat.Find(txtPosNroUpdDel.Text);

            if (post != null)
            {
                entities.Postitoimipaikat.Remove(post);
                entities.SaveChanges();
            }

            HaePostitoimipaikat();
            txtPosNroUpdDel.Text = "";
            txtPosTmipUpdDel.Text = "";
        }

        private void BtnPäivitäPTmip_Click(object sender, RoutedEventArgs e)
        {

            //entities.SaveChanges();     //Jos korjaus tehdään listassa tarvitaan tähän clickiin vain tämä rivi!!!

            //tietojen muokkaus textboxissa, kun tiedot on ensin valittu listalta
            string pnro = txtPosNroUpdDel.Text;

            Postitoimipaikat post = (from p in entities.Postitoimipaikat
                                    where p.Postinumero == pnro
                                    select p).FirstOrDefault();

            post.Postinumero = txtPosNroUpdDel.Text;
            post.Postitoimipaikka = txtPosTmipUpdDel.Text;

            
            entities.SaveChanges();

            HaePostitoimipaikat();
            txtPosNroUpdDel.Text = "";
            txtPosTmipUpdDel.Text = "";

        }

        private void BtnSuljePtmip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
