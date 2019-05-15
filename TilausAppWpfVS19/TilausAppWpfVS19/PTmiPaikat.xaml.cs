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
    /// Interaction logic for PTmiPaikat.xaml
    /// </summary>
    public partial class PTmiPaikat : Window
    {
        public PTmiPaikat()
        {
            InitializeComponent();
        }

        private void BtnSuljePTmiPaikat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
