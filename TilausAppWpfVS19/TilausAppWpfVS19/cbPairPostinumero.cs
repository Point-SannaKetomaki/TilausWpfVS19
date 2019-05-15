using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilausAppWpfVS19
{
    class cbPairPostinumero
    {
        public string postiNumero { get; set; }
        public string postiToimipaikka { get; set; }


        public cbPairPostinumero(string PostiNumero, string PostiToimipaikka)
        {
            postiNumero = PostiNumero;
            postiToimipaikka = PostiToimipaikka;
        }
    }
}
