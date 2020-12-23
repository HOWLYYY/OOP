namespace Race
{
    class Mortar : AirTransport
    {
        public Mortar() : base("ступа", 8) { }
        public override double GetDistance(double distance)
        {
            return distance * 0.94;
        }
    }
}
