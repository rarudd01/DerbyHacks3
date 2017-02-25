using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace DerbyHacksApi.Models
{
    public class Block
    {
        //Top bounding coords
        public double TopLatitude;
        public double TopLongitude;

        //bottom bounding coords
        public double BottomLatitude;
        public double BottomLongitude;

        public List<Incident> Incidents;

        //list of ratios and counts of various crimes
        public List<CrimeRatio> CrimeRatios;
        //the threat level
        public double Threat;

        //method to calculate threat
        public void CalculateThreat()
        {
            //give weights to each crime type
            //add them together
        }

        public void CalculateCrimeRatios()
        {
            //calculate total number of crimes
            //for each crime type
                //count the total of crimes
                //divide and store ratio

        }

    }
}