using System;
using System.Collections.Generic;
using System.Text;

namespace Race
{
    abstract class Race
    {
        public Track track;
        public List<Result> results;

        public Race(Track track)
        {
            this.track = track;
            results = new List<Result>();
        }

        public abstract void AddRacer(Transport Racer);
        
        public void Start()
        {
            foreach(var result in results)
            {
                result.RacerTime = result.Racer.TimeForDistance(track.Distance);
            }
        }

        private int comparer(Result x, Result y)
        {
            return (x.RacerTime - y.RacerTime > 0 ? 1 : -1);
        }

        public void Result() 
        {
            results.Sort(comparer);
        }

        public void PrintResult()
        {
            foreach(var result in results)
            {
                Console.WriteLine("{0} : {1}", result.Racer, result.RacerTime);
            }
        }
    }

    class Result
    {
        public Transport Racer;
        public double RacerTime;
        public Result(Transport Racer, double RacerTime = 0)
        {
            this.Racer = Racer;
            this.RacerTime = RacerTime;
        }
    }
}
