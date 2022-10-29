using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb3Bordsbokning
{
    public class BokningsBord
    {
        public BokningsBord(int bordNo, string namn)
        {
            this.number = bordNo;
            this.namn = namn;
        }

        public int number { get; private set; }
        public string namn { get; private set; }

        public bool BokingThisTable(string name)
        {
            if (IsBoked())
            {
                //gibt false weil tisch gebucht, messagebox in ui
                return false;
            }
            else
            {
                namn = name;
                return true;
            }
        }

        public bool CancelThisTable(string name)
        {
            if(namn == name)
            {
                namn = "";
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsBoked()
        {
            if(namn == "")
            {
                return false;
            }
            return true;
        }
    }
}
