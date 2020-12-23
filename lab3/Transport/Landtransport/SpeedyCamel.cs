namespace Race
{
    class SpeedyCamel : LandTransport
    {
        public SpeedyCamel() : base("Быстрый верблюд", 40, 10) { }

        public override double RestTime(double time)
        {
            int RestCounter = (int)(time / restTime);
            time = 0;
            if (RestCounter > 0)
                time += 5;
            if (RestCounter > 1)
                time += 6.5;
            if (RestCounter > 2)
                time += 8 * (RestCounter - 2);
            return time;
        }
    }
}
