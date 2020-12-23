using System;
using System.Collections.Generic;
using System.Text;

namespace Race
{
    class AllTerrainBoots : LandTransport
    {
        public AllTerrainBoots() : base("Ботинки-вездеходы", 6, 60) { }

        public override double RestTime(double time)
        {
            int RestCounter = (int)(time / restTime);
            time = 0;
            if (RestCounter > 0)
                time += 10;
            if (RestCounter > 1)
                time += 5 * (RestCounter - 1);
            return time;
        }
    }
}
