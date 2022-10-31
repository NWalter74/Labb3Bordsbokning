using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    /// <summary>
    /// A day in a kalender contains the date and a list of times.
    /// So does even this class.
    /// </summary>
    public class BokningsDag
    {
        public BokningsDag(string datum)
        {
            this.datum = datum;
        }

        public string datum { get; private set; }

        public List<BokningsTid> tidLista = new List<BokningsTid>();

        /// <summary>
        /// This method adds your time to a list of times and returns true
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
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
