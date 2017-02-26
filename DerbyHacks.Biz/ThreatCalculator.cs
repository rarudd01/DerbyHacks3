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
            map.Add("Arson".ToUpper(), 0);
            map.Add("Assault".ToUpper(), 0);
            map.Add("Burgulary".ToUpper(), 0);
            map.Add("DisturbingThePeace".ToUpper(), 0);
            map.Add("Drugs".ToUpper(), 0);
            map.Add("Dui".ToUpper(), 0);
            map.Add("Fraud".ToUpper(), 0);
            map.Add("Homicide".ToUpper(), 0);
            map.Add("MotorVehicleTheft".ToUpper(), 0);
            map.Add("Other".ToUpper(), 0);
            map.Add("Robbery".ToUpper(), 0);
            map.Add("SexCrimes".ToUpper(), 0);
            map.Add("Theft".ToUpper(), 0);
            map.Add("Vandalism".ToUpper(), 0);
            map.Add("VehicleBreakIn".ToUpper(), 0);
            map.Add("Weapons".ToUpper(), 0);

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
            foreach (KeyValuePair<string, int> entry in map)
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
            map.Add("Arson".ToUpper(), ThreatType.Arson);
            map.Add("Assault".ToUpper(), ThreatType.Assault);
            map.Add("Burgulary".ToUpper(), ThreatType.Burgulary);
            map.Add("DisturbingThePeace".ToUpper(), ThreatType.DisturbingThePeace);
            map.Add("Drugs".ToUpper(), ThreatType.Drugs);
            map.Add("Dui".ToUpper(), ThreatType.Dui);
            map.Add("Fraud".ToUpper(), ThreatType.Fraud);
            map.Add("Homicide".ToUpper(), ThreatType.Homicide);
            map.Add("MotorVehicleTheft".ToUpper(), ThreatType.MotorVehicleTheft);
            map.Add("Other".ToUpper(), ThreatType.Other);
            map.Add("Robbery".ToUpper(), ThreatType.Robbery);
            map.Add("SexCrimes".ToUpper(), ThreatType.SexCrimes);
            map.Add("Theft".ToUpper(), ThreatType.Theft);
            map.Add("Vandalism".ToUpper(), ThreatType.Vandalism);
            map.Add("VehicleBreakIn".ToUpper(), ThreatType.Vandalism);
            map.Add("Weapons".ToUpper(), ThreatType.Weapons);

            foreach (CrimeData indident in block.Incidents)
            {
                ThreatType scalar;
                if (map.TryGetValue(indident.CrimeType.ToUpper(), out scalar))
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
