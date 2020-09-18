using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class GridReference
    {
        public int Easting { get; set; }

        public int Northing { get; set; }

        public static GridReference GetGridReference(int easting, int northing)
        {
            return new GridReference() { Easting = easting, Northing = northing };
        }

        public static GridReference GetGridReference(string easting, string northing)
        {
            if (int.TryParse(easting.Replace("E", ""), out int E) && int.TryParse(northing.Replace("N", ""), out int N))
            {
                return new GridReference() { Easting = E, Northing = N };
            }
            return new GridReference();
        }
    }
}
