namespace Race
{
    class HoverCarpet : AirTransport
    {
        public HoverCarpet() : base("ковер-самолет", 10) { }
        public override double GetDistance(double distance)
        {
            if (distance < 1000)
                return distance;
            if (distance < 5000)
                return distance * 0.97;
            if (distance < 10000)
                return distance * 0.9;
            else
                return distance * 0.95;
        }
    }
}
