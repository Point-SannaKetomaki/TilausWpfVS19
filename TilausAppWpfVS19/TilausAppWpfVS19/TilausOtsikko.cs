﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilausAppWpfVS19
{
    class TilausOtsikko
    {
        private DateTime tilausPvm;
        private DateTime toimitusPvm;

        public int TilausNumero { get; set; }
        public int AsiakasNumero { get; set; }
        public string ToimitusOsoite { get; set; }
        public string Postinumero { get; set; }

        public DateTime TilausPvm
        {
            get
            {
                return tilausPvm;
            }
            set
            {
                if (value <= DateTime.Today)
                {
                    tilausPvm = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Tilauspäivä ei voi olla tulevaisuudessa");
                }
            }
        }
        public DateTime ToimitusPvm
        {
            get
            {
                return toimitusPvm;
            }
            set
            {
                if (value >= DateTime.Today)
                {
                    toimitusPvm = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Toimituspäivä ei voi olla menneisyydessä");
                }
            }
        }

        internal string LaskeToimitusAika()
        {
            TimeSpan erotus;
            erotus = toimitusPvm.Subtract(tilausPvm);
            return erotus.Days.ToString();
        }
    }
}
