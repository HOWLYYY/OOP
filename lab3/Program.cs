using System;

namespace Race
{
    class Program
    {
        static void Main(string[] args)
        {
            Track track = new Track(1000);
            Race race = MakeAirRace(track);
            race.Start();
            race.Result();
            race.PrintResult();
        }
        public static AirRace MakeAirRace(Track track)
        {
            AirRace airRace = new AirRace(track);
            airRace.AddRacer(new Mortar());
            airRace.AddRacer(new HoverCarpet());
            airRace.AddRacer(new Broom());
            return airRace;
        }

        public static LandRace MakeLandRace(Track track)
        {
            LandRace landRace = new LandRace(track);
            landRace.AddRacer(new BactriaCamel());
            landRace.AddRacer(new SpeedyCamel());
            landRace.AddRacer(new Centaur());
            return landRace;
        }

        public static MixedRace MakeMixedRace(Track track)
        {
            MixedRace mixedRace = new MixedRace(track);
            mixedRace.AddRacer(new BactriaCamel());
            mixedRace.AddRacer(new AllTerrainBoots());
            mixedRace.AddRacer(new Broom());
            mixedRace.AddRacer(new Mortar());
            return mixedRace;
        }
    }
}
