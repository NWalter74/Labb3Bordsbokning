using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    public class BokningsDagar
    {
        public string datum { get; private set; }
        public string tid { get; private set; }

        List<Dag> listaAvBokadeDagar = new List<Dag>();

        public BokningsDagar(string kundDatum, string kundTid)
        {
            this.datum = kundDatum;
            this.tid = kundTid;
        }

        public void CancelThisDay(string listboxDatum, string listboxTid)
        {
            //Krav[14]
            var result = listaAvBokadeDagar.Where(item => item.datum == listboxDatum && item.tid == listboxTid);

            if (result != null)
            {
                listaAvBokadeDagar.Remove(result.First());
            }
        }

        public Dag SaveThisBokingDay(string kundDatum, string kundTid)
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
