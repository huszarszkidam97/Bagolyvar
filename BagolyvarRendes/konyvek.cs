using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagolyvarRendes
{
    class konyvek
    {
        string kod;
        string szerzo;
        string cim;
        int kiadaseve;
        int ar;
        string isbn;

        public konyvek(string kod, string szerzo, string cim, int kiadaseve, int ar, string isbn)
        {
            this.Kod = kod;
            this.Szerzo = szerzo;
            this.Cim = cim;
            this.Kiadaseve = kiadaseve;
            this.ar = ar;
            this.Isbn = isbn;
        }

        public string Isbn { get => isbn; set => isbn = value; }
        public int Ar { get => ar; set => ar = value; }
        public int Kiadaseve { get => kiadaseve; set => kiadaseve = value; }
        public string Cim { get => cim; set => cim = value; }
        public string Szerzo { get => szerzo; set => szerzo = value; }
        public string Kod { get => kod; set => kod = value; }
    }
}
