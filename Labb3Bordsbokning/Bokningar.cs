using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb3Bordsbokning.BokningsDagar;

namespace Labb3Bordsbokning
{
    public class Bokningar
    {
        public Bokningar(BokningsDagar.Dag dag, BokningsBord.Bord bord)
        {
            this.dag = dag;
            this.bord = bord;
        }

        BokningsDagar.Dag dag { get; set; }
        BokningsBord.Bord bord { get; set; }

        public Bokning SaveBoking(BokningsDagar.Dag resultDag, BokningsBord.Bord resultBord)
        {
            Bokning bokning = new Bokning();
            bokning.dag = resultDag;
            bokning.bord = resultBord;

            return bokning;
        }

        public struct Bokning
        {
            public BokningsDagar.Dag dag;
            public BokningsBord.Bord bord;

        }
    }
}
