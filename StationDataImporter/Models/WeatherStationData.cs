using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherStationData
    {

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TMax { get; set; }

        public decimal TMin { get; set; }

        public int FrostDays { get; set; }

        public decimal Rainfall { get; set; }

        public decimal Sunshine { get; set; }

        public static WeatherStationData GetWeatherStationData(string Year, string Month, string TMax, string TMin, string FrostDays, string Rainfall, string Sunshine)
        {
            var wsd = new WeatherStationData();
            if (int.TryParse(Year, out int y)) wsd.Year = y;
            if (int.TryParse(Month, out int m)) wsd.Month = m;
            if (decimal.TryParse(TMax, out decimal tmax)) wsd.TMax = tmax;
            if (decimal.TryParse(TMin, out decimal tmin)) wsd.TMin = tmin;
            if (int.TryParse(FrostDays, out int fd)) wsd.FrostDays = fd;
            if (decimal.TryParse(Rainfall, out decimal r)) wsd.Rainfall = r;
            if (decimal.TryParse(Sunshine, out decimal s)) wsd.Sunshine = s;
            return wsd;
        }
    }
}
