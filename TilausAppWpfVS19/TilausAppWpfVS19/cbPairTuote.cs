using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilausAppWpfVS19
{
    class cbPairTuote
    {

        public string tuoteNimi { get; set; }
        public int tuoteNumero { get; set; }
        
       
        public cbPairTuote(string TuoteNimi, int TuoteNumero)
        {
            tuoteNimi = TuoteNimi;
            tuoteNumero = TuoteNumero;
        }
    }
}
