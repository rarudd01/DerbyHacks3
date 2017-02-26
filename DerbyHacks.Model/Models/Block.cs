using DerbyHacksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerbyHacks.Model
{
    public class Block : IDataModel
    {

        public List<CrimeData> Incidents;

        //list of ratios and counts of various crimes
        public List<CrimeRatio> CrimeRatios;
        //the threat level
        public int Threat;

        public Block(double latitude, double longitude)
        {
            DataHelper data = new DataHelper();
            Incidents = data.GetIncidentsInRange(latitude, longitude, 0.002814).ToList();
        }
    }
}