namespace Race
{
    class Track
    {
        public double Distance { get; private set; }

        public Track(double Distance = 0)
        {
            if (Distance < 0)
                throw new System.Exception("Distance is negative");
            else
                this.Distance = Distance;
        }
    }
}
