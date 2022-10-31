using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    public class BokningsDag
    {
        public string datum { get; private set; }
        public string tid { get; private set; }

        List<Dag> listaAvBokadeDagar = new List<Dag>();

        public BokningsDag(string kundDatum, string kundTid)
        {
            this.datum = kundDatum;
            this.tid = kundTid;
        }

        public Dag SparaDennaBokningDag(string kundDatum, string kundTid)
        {
            Dag dag = new Dag();
            dag.datum = kundDatum;
            dag.tid = kundTid;

            listaAvBokadeDagar.Add(dag);
            return dag;

        }

        public struct Dag
        {
            public string datum;
            public string tid;
        }
    }
}
