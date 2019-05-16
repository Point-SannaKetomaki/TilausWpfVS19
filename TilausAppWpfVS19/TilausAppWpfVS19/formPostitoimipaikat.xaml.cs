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

        private void BtnSuljePtmip_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    txtPosNroPoista.Text = Postinumero.Text;
                }

                //haetaan valitun rivin toisen sarakkeen sisältö
                TextBlock Postitoimipaikka = dgPostitoimipaikat.Columns[1].GetCellContent(
                    dgPostitoimipaikat.Items[dgPostitoimipaikat.SelectedIndex]) as TextBlock;
                if (Postitoimipaikka != null)
                {
                    txtPosTmipPoista.Text = Postitoimipaikka.Text;
                }
            }
            
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

        private void BtnPoistaPTmip_Click(object sender, RoutedEventArgs e)
        {
            Postitoimipaikat post = entities.Postitoimipaikat.Find(txtPosNroPoista.Text);

            if (post != null)
            {
                entities.Postitoimipaikat.Remove(post);
                entities.SaveChanges();
            }

            HaePostitoimipaikat();
            txtPosNroPoista.Text = "";
            txtPosTmipPoista.Text = "";
        }
    }
}
