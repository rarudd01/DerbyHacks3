using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SQLite;
using DerbyHacksApi.Models;
namespace DerbyHacksApi.Controllers
{
    public class CrimeController : ApiController
    {
        public Block Get(HttpRequestMessage req)
        {
            
            double latitude;
            double longitude;

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

            Block block = new Block();


            return block;
        }
    }
}