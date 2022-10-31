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
        public List<string> bokadTidList = new List<string>();

        public BokningsDag(string datum)
        {
            this.datum = datum;
        }
    }
}
