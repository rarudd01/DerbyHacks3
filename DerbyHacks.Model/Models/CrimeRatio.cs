using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerbyHacks.Model
{
    public class CrimeRatio : IDataModel
    {
        public CrimeRatio(string type, int count, int total)
        {
            CrimeType = type;
            Count = count;
            Ratio = count / total;
        }
        public Dictionary<string, int> CrimeChart;
        public string CrimeType;
        public double Ratio;
        public int Count;
    }
}