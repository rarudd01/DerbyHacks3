using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerbyHacks.Biz
{
    public class ThreatCalculator
    {
        private Dictionary<ThreatLevel, int> threatThresholds;

        private Block block;

        public ThreatLevel ThreatLevel
        {
            get
            {
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

        public ThreatCalculator(Block _block)
        {
            block = _block;
            threatThresholds = new Dictionary<ThreatLevel, int>();
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

        public void CalculateCrimeRatios()
        {
            int count = 0;

            Dictionary<string, int> map = new Dictionary<string, int>();
            map.Add("Arson", 0);
            map.Add("Assault", 0);
            map.Add("Burgulary", 0);
            map.Add("DisturbingThePeace", 0);
            map.Add("Drugs", 0);
            map.Add("Dui", 0);
            map.Add("Fraud", 0);
            map.Add("Homicide", 0);
            map.Add("MotorVehicleTheft", 0);
            map.Add("Other", 0);
            map.Add("Robbery", 0);
            map.Add("SexCrimes", 0);
            map.Add("Theft", 0);
            map.Add("Vandalism", 0);
            map.Add("VehicleBreakIn", 0);
            map.Add("Weapons", 0);

            foreach (CrimeData indident in block.Incidents)
            {
                count++;
                int currentCount = 0;
                if (!map.TryGetValue(indident.CrimeType, out currentCount))
                {
                    map.Add(indident.CrimeType, currentCount);
                }
                else
                {
                    map[indident.CrimeType] = currentCount++;
                }
            }

            List<CrimeRatio> crimeRatios = new List<CrimeRatio>();
            foreach(KeyValuePair<string, int> entry in map)
            {
                CrimeRatio ratio = new CrimeRatio(entry.Key, entry.Value, count);
                crimeRatios.Add(ratio);
            }

            block.CrimeRatios = crimeRatios;
        }
        private int calculate()
        {
            int currentThreatLevel = 0;
            Dictionary<string, ThreatType> map = new Dictionary<string, ThreatType>();
            map.Add("Arson", ThreatType.Arson);
            map.Add("Assault", ThreatType.Assault);
            map.Add("Burgulary", ThreatType.Burgulary);
            map.Add("DisturbingThePeace", ThreatType.DisturbingThePeace);
            map.Add("Drugs", ThreatType.Drugs);
            map.Add("Dui", ThreatType.Dui);
            map.Add("Fraud", ThreatType.Fraud);
            map.Add("Homicide", ThreatType.Homicide);
            map.Add("MotorVehicleTheft", ThreatType.MotorVehicleTheft);
            map.Add("Other", ThreatType.Other);
            map.Add("Robbery", ThreatType.Robbery);
            map.Add("SexCrimes", ThreatType.SexCrimes);
            map.Add("Theft", ThreatType.Theft);
            map.Add("Vandalism", ThreatType.Vandalism);
            map.Add("VehicleBreakIn", ThreatType.Vandalism);
            map.Add("Weapons", ThreatType.Weapons);

            foreach(CrimeData indident in block.Incidents)
            {
                ThreatType scalar;
                if(map.TryGetValue(indident.CrimeType, out scalar))
                {
                    currentThreatLevel = currentThreatLevel + (int)scalar;
                }
            }
            return currentThreatLevel;

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
        Assault = 11,
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
