namespace Race
{
    class MixedRace : Race
    {
        public MixedRace(Track track) : base(track)
        { }
        public override void AddRacer(Transport Racer)
        {
            results.Add(new Result(Racer));
        }
    }
}
