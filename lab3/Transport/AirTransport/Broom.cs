using System; 

namespace Race
{
    class Broom : AirTransport
    {
        public Broom() : base("метла", 20) { }
        public override double GetDistance(double distance)
        {
            double BDistance = distance * (Math.Floor(distance / 1000) * 0.99);
            return BDistance;
        }
    }
}
