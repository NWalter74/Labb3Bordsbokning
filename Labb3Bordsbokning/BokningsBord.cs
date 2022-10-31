using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb3Bordsbokning.BokningsDagar;

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

        public void CancelThisBord(string listboxNamn, int listboxBordNummer)
        {
            //Krav[14]
            var result = listaAvBokadeBord.Where(item => item.namn == listboxNamn && item.nummer == listboxBordNummer);
            
            if (result != null)
            {
                listaAvBokadeBord.Remove(result.First());
            }
        }

        public Bord SaveThisTable(int kundBordNo, string kundNamn)
        {
            Bord bord = new Bord();
            bord.nummer = kundBordNo;
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
