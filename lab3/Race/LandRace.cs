namespace Race
{
    class LandRace : Race
    {
        public LandRace(Track track) : base(track)
        { }
        public override void AddRacer(Transport Racer)
        {
            if (Racer.GetType().IsSubclassOf(typeof(LandTransport)))
            {
                results.Add(new Result(Racer));
            }
            else
                throw new System.Exception("wrong type of racer");
        }
    }
}
