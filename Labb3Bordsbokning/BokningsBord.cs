using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    public class BokningsBord
    {
        public int bordNo { get; private set; }
        public string kundNamn { get; private set; }

        public BokningsBord(int bordNo, string kundNamn)
        {
            this.bordNo = bordNo;
            this.kundNamn = kundNamn;
        }

    }
}
