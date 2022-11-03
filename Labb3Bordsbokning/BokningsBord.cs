using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private List<Bord> listaAvBokadeBord = new List<Bord>();

        public BokningsBord(int bordNo, string kundNamn)
        {
            this.bordNo = bordNo;
            this.kundNamn = kundNamn;
        }

        /// <summary>
        /// Sparar ett bord som består av ett namn och en nummer
        /// </summary>
        /// <param name="kundBordNo"></param>
        /// <param name="kundNamn"></param>
        /// <returns></returns>
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
