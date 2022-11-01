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

        private List<Dag> listaAvBokadeDagar = new List<Dag>();

        public BokningsDagar(string kundDatum, string kundTid)
        {
            this.datum = kundDatum;
            this.tid = kundTid;
        }

        /// <summary>
        /// Sparar en dag som består av en tid och ett datum
        /// </summary>
        /// <param name="kundDatum"></param>
        /// <param name="kundTid"></param>
        /// <returns></returns>
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
