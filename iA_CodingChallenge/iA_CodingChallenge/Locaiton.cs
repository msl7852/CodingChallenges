using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iA_CodingChallenge
{
    public class Location
    {
        public int xLoc { get; set; }
        public int yLoc { get; set; }

        public Location(int x, int y)
        {
            xLoc = x;
            yLoc = y;
        }
    }
}
