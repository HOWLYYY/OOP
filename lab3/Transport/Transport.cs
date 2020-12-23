namespace Race
{
    abstract class Transport
    {
        public double Speed { get; }
        public string Name { get; }
        public Transport(string Name, double Speed)
        {
            this.Name = Name;
            this.Speed = Speed;
        }

        public abstract double TimeForDistance(double distance);
    }
}
