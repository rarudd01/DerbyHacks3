using DerbyHacks.Model;
using DerbyHacksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerbyHacks.Biz
{
    public class ThreatCalculator
    {
        private Dictionary<ThreatLevel, int> threatThresholds;

        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public ThreatLevel ThreatLevel
        {
            get
            {
                if (Radius <= 0)
                {
                    throw new InvalidOperationException("Please set valid crime radius.");
                }

                if (threatThresholds.Count == 0)
                {
                    throw new InvalidOperationException("Please add threshold.");
                }

                int severity = calculate();
                
                foreach (var item in threatThresholds.OrderByDescending(t => t.Key))
                {
                    if (item.Value < severity)
                    {
                        return item.Key;
                    }
                }

                return ThreatLevel.None;
            }
        }

        public ThreatCalculator(double latitude, double longitude, double radius)
        {
            threatThresholds = new Dictionary<ThreatLevel, int>();
            Radius = radius;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void AddThreshold(ThreatLevel level, int value)
        {
            int currentVal;

            if (level == ThreatLevel.None)
            {
                return;
            }

            if (!threatThresholds.TryGetValue(level, out currentVal))
            {
                threatThresholds.Add(level, value);
            }
            else
            {
                threatThresholds[level] = value;
            }
        }

        private int calculate()
        {
            DataHelper helper = new DataHelper();

            IEnumerable<CrimeData> data = helper.GetIncidentsInRange(Longitude, Latitude, Radius);
            Dictionary<string, ThreatType> map = new Dictionary<string, ThreatType>();
            map.Add("Arson", ThreatType.Arson);
            throw new NotImplementedException();
        }
    }

    public enum ThreatLevel
    {
        None = 0,
        Low = 1,
        Moderate = 2,
        High = 3,
        Critical = 4
    }

    public enum ThreatType
    {
        Arson = 7,
        Assualt = 11,
        Burgulary = 12,
        DisturbingThePeace = 4,
        Drugs = 6,
        Dui = 10,
        Fraud = 2,
        Homicide = 15,
        MotorVehicleTheft,
        Other = 1, 
        Robbery = 13,
        SexCrimes = 14,
        Theft = 9,
        Vandalism = 3,
        VehicleBreakIn = 5,
        Weapons = 8
    }
}
