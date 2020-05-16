using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagolyvarRendes
{
    class berlok
    {
        int id;
        string nev;

        public berlok(int id, string nev)
        {
            this.id = id;
            this.Nev = nev;
        }

        public string Nev { get => nev; set => nev = value; }
        public int Id { get => id; set => id = value; }
        public override string ToString()
        {
            return nev;
        }
    }
}
