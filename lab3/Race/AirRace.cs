namespace Race
{
    class AirRace : Race
    {
        public AirRace(Track track) : base(track)
        {}

        public override void AddRacer(Transport Racer)
        {
            if (Racer.GetType().IsSubclassOf(typeof(AirTransport)))
            {
                results.Add(new Result(Racer));
            }
            else
                throw new System.Exception("wrong type of racer");
        }

    }
}
 