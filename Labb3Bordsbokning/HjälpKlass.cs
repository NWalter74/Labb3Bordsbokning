using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3Bordsbokning
{
    public class HjälpKlass
    {
        public List<BokingRecord> bokingRecordsList = new List<BokingRecord>();

        public HjälpKlass(string[] lines)
        {
            foreach (var item in lines)
            {
                string[] result = item.ToString().Split(',');
                string Datum = result[0].Trim();
                string Tid = result[1].Trim();
                string Namn = result[2].Trim();
                int BordNummer = int.Parse(result[3].Substring(5).Trim());

                CreateObjekt(Datum, Tid, Namn, BordNummer);
            }
        }

        public void CreateObjekt(string datum, string tid, string namn, int bordNummer)
        {
            BokingRecord bokingRecord = new BokingRecord();
            bokingRecord.datum = datum;
            bokingRecord.tid = tid;
            bokingRecord.namn = namn;
            bokingRecord.nummer = bordNummer;

            bokingRecordsList.Add(bokingRecord);
        }

        public struct BokingRecord
        {
            public string datum;
            public string tid;
            public int nummer;
            public string namn;
        }
    }
}
