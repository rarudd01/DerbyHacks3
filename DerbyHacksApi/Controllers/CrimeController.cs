using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SQLite;
using DerbyHacks.Model;
using DerbyHacks.Biz;
namespace DerbyHacksApi.Controllers
{
    public class CrimeController : ApiController
    {
        public Block Get(HttpRequestMessage req)
        {
            
            double latitude = 0;
            double longitude = 0;

            IEnumerable<string> headerValues = req.Headers.GetValues("latitude");
            if(req.Headers.Contains("latitude"))
            {
                latitude = Convert.ToDouble(headerValues.FirstOrDefault());
            }

            headerValues = req.Headers.GetValues("longitude");
            if (req.Headers.Contains("longitude"))
            {
                longitude = Convert.ToDouble(headerValues.FirstOrDefault());
            }

            Block block = new Block(latitude, longitude);

            ThreatCalculator calculator = new ThreatCalculator(block);

            calculator.CalculateCrimeRatios();
            calculator.AddThreshold(ThreatLevel.Low, 15);
            calculator.AddThreshold(ThreatLevel.Moderate, 30);
            calculator.AddThreshold(ThreatLevel.High, 50);
            calculator.AddThreshold(ThreatLevel.Critical, 100);

            block.Threat = (int)calculator.ThreatLevel;


            return block;
        }
    }
}
