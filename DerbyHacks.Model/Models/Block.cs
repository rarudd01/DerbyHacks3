using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using DerbyHacksApi.Models;
using DerbyHacks.Model;

namespace DerbyHacksApi.Models
{
    public class Block : IDataModel
    {
        
        //Top bounding coords
        public double TopLatitude;
        public double TopLongitude;

        //bottom bounding coords
        public double BottomLatitude;
        public double BottomLongitude;

        public List<CrimeData> Incidents;

        //list of ratios and counts of various crimes
        public List<CrimeRatio> CrimeRatios;
        //the threat level
        public double Threat;

        public Block(double latitude, double longitude)
        {
            DataHelper data = new DataHelper();
            Incidents = data.GetIncidentsInRange(latitude, longitude, .005).ToList();
            CalculateThreat();
            CalculateCrimeRatios();
        }

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