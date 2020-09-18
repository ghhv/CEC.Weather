using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherStationDataSet
    {
        public string Station { get; set; }

        public string GUID { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public List<WeatherStationData> Data { get; set; } = new List<WeatherStationData>();
    }
}
