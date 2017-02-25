using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerbyHacksApi.Models
{
    public class CrimeRatio : IDataModel
    {
        public string CrimeType;
        public double Ratio;
        public int Count;
    }
}