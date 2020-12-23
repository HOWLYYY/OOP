using System;
using System.Collections.Generic;
using System.Text;

namespace Race
{
    abstract class LandTransport : Transport
    {
        public double restTime { get; set; }
        public LandTransport(string Name, double Speed, double restTime) : base (Name, Speed)
        {
            this.restTime = restTime;
        }

        public abstract double RestTime(double time);

        public override double TimeForDistance(double distance)
        {
            double time = distance / Speed;
            return time + RestTime(time);
        }
    }
}
