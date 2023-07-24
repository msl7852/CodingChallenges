using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iA_CodingChallenge
{
    public class CentralFillFacility
    {
        public Location centerLocation;
        public List<Medication> Inventory = new List<Medication>();
        public string uniqueId;

        public CentralFillFacility(Location loc, string id)
        {
            centerLocation = loc;
            Inventory.Add(new Medication("A", 1, 100));
            Inventory.Add(new Medication("B", 1, 100));
            Inventory.Add(new Medication("C", 1, 100));
            uniqueId = id;
        }

        public Medication getCheapestMedication()
        {
            return Inventory.OrderBy(m => m.Cost).First();
        }
    }
}
