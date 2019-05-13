using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilausAppWpfVS19
{
    class cbPairAsiakas
    {
        public string asiakasNimi { get; set; }
        public int asiakasNumero { get; set; }
        public cbPairAsiakas(string AsiakasNimi, int AsiakasNumero)
        {
            asiakasNimi = AsiakasNimi;
            asiakasNumero = AsiakasNumero;
        }
    }
}
