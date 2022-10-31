using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb3Bordsbokning.BokningsDag;

namespace Labb3Bordsbokning
{
    public class Bokningar
    {
        public Bokningar(BokningsDag.Dag dag, BokningsBord.Bord bord)
        {
            this.dag = dag;
            this.bord = bord;
        }

        BokningsDag.Dag dag { get; set; }
        BokningsBord.Bord bord { get; set; }


        public Bokning SparaBokning(BokningsDag.Dag resultDag, BokningsBord.Bord resultBord)
        {
            Bokning bokning = new Bokning();
            bokning.dag = resultDag;
            bokning.bord = resultBord;

            return bokning;
        }

        public struct Bokning
        {
            public BokningsDag.Dag dag;
            public BokningsBord.Bord bord;
        }
    }
}
