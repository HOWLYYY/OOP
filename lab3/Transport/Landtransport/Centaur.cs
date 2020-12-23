namespace Race
{
    class Centaur : LandTransport
    {
        public Centaur() : base("Кентавр", 15, 8) { }

        public override double RestTime(double time)
        {
            int RestCounter = (int)(time / restTime);
            time = 0;
            if (RestCounter > 0)
                time += 2 * (RestCounter);
            return time;
        }
    }
}
