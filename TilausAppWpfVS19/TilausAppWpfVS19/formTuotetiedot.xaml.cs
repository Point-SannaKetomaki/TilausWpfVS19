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
        public formTuotetiedot()
        {
            InitializeComponent();
        }

        private void BtnSuljeTuotetiedot_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
