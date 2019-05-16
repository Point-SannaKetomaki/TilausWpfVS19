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
            //TODO: Tähän vähän koodia
        }

        private void BtnLisaaPtmip_Click(object sender, RoutedEventArgs e)
        {
            Postitoimipaikat post = new Postitoimipaikat();
            post.Postinumero = txtPosNro.Text;
            post.Postitoimipaikka = txtPosTmip.Text;
            entities.Postitoimipaikat.Add(post);
            entities.SaveChanges();

            HaePostitoimipaikat();
        }
    }
}
