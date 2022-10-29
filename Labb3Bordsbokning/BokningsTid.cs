using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    public class BokningsTid 
    {
        //wenn objekt zeit dann immer mit 10 tischobjekten

        public BokningsBord[] bordArray = new BokningsBord[10];
        public string tid { get; private set; }


        public BokningsTid(string tid)
        {
            this.tid = tid;

            for(int i = 0; i < 10; i++)
            {
                bordArray[i] = new BokningsBord(i + 1, "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bordNummer">bordnummer between 1-10</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool BokingATable(int bordNummer, string name)
        {
            return bordArray[bordNummer - 1].BokingThisTable(name);

        }

        public bool CancelTable(string name, int bordNummer)
        {
            return bordArray[bordNummer - 1].CancelThisTable(name);
        }

    }
}
