﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerbyHacks.Model
{
    public class CrimeData : IDataModel
    {
        public int Id { get; set; }
        public DateTime DateOccured { get; set; }
        public string CrimeType { get; set; }
        public string BlockAddress { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string IncidentNumber { get; set; }
        public string State { get; set; }
        public string FormattedAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
