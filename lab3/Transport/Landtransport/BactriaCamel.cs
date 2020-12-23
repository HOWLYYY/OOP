namespace Race
{
    class BactriaCamel : LandTransport
    {
        public BactriaCamel() : base("Двугорбный верблюд", 10, 30) { }

        public override double RestTime(double time)
        {
            int RestCounter = (int)(time / restTime);
            time = 0;
            if (RestCounter > 0)
                time += 5;
            if (RestCounter > 1)
                time += 8 * (RestCounter - 1);
            return time;
        }
    }
}
