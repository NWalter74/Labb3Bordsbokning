using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb3Bordsbokning.BokningsDag;

namespace Labb3Bordsbokning
{
    public class BokningsBord
    {
        public int bordNo { get; private set; }
        public string kundNamn { get; private set; }

        List<Bord> listaAvBokadeBord = new List<Bord>();

        public BokningsBord(int bordNo, string kundNamn)
        {
            this.bordNo = bordNo;
            this.kundNamn = kundNamn;
        }

        public Bord SparaDennaBord(int kundBord, string kundNamn)
        {
            Bord bord = new Bord();
            bord.nummer = kundBord;
            bord.namn = kundNamn;

            listaAvBokadeBord.Add(bord);
            return bord;
        }

        public struct Bord
        {
            public int nummer;
            public string namn;
        }


    }
}
