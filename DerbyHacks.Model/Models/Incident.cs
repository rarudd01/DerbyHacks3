using DerbyHacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerbyHacksApi.Models
{
    public class Incident : IDataModel
    {
        double Latitude;
        double Longitude;
        string CrimeType;

    }
}