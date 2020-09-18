using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class GPSLocation
    {
        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public decimal Elevation { get; set; }

        public static GPSLocation GetGPSLocation(decimal longitude, decimal latitude, decimal elevation)
        {
            return new GPSLocation() { Longitude = longitude, Latitude = latitude, Elevation = elevation };
        }
        
        public static GPSLocation GetGPSLocation(string latitude, string longitude , string elevation)
        {
            if (decimal.TryParse(longitude, out decimal lon) && decimal.TryParse(latitude, out decimal lat) && decimal.TryParse(elevation, out decimal el))
            {
                return new GPSLocation() { Longitude = lon, Latitude = lat, Elevation = el };
            }
            return new GPSLocation();
        }

    }
}
