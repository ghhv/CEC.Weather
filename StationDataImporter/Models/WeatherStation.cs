using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherStation
    {
        public string Station { get; set; }

        public long ID { get; set; }

        public string GUID { get; set; }

        public GridReference GridData { get; set; }

        public GPSLocation Location { get; set; }

        [JsonPropertyName("LastUpdate")]
        public string LastUpdateString { get; set; }

        public DateTime LastUpdated { get => DateTime.Parse(this.LastUpdateString); }

        public string DataUrl { get; set; }

        public string SourceUrl { get; set; }
    }
}
