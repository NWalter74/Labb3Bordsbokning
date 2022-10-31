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
        public List<string> tidLista = new List<string>() { "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00"};
    }
}
