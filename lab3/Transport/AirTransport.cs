namespace Race
{
    abstract class AirTransport : Transport
    {
        public AirTransport(string Name, double Speed) : base(Name, Speed)
        {}

        public abstract double GetDistance(double distance);

        public override double TimeForDistance(double distance)
        {
            double time = GetDistance(distance) / Speed;
            return time;
        }
    }
}
