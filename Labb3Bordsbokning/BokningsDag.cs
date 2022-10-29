using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    public class BokningsDag
    {
        public BokningsDag(string datum)
        {
            this.datum = datum;
        }

        public string datum { get; private set; }

        public List<BokningsTid> tidLista = new List<BokningsTid>();

        public bool BokingOfTime(BokningsTid tid)
        {
            //hier auch pruefen ob zeit schon vergeben
            tidLista.Add(tid);
            return true;
        }

        public bool CancelTime(string tid, string name, int bordNummer)
        {
            var result = tidLista.Where(item => item.tid == tid).First();
            
            result.CancelTable(name, bordNummer);
            return true;
        }
    }
}
