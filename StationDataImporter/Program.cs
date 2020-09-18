using System;
using WeatherApp.Models;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = string.Empty;
            string stationurl = "https://www.metoffice.gov.uk/pub/data/weather/uk/climate/stationdata/tireedata.txt";
            int stationid = 1;
            Console.WriteLine("Weather Station Importer");

            Console.Write("StationUrl?");
            stationurl = Console.ReadLine();

            value = string.Empty;
            Console.Write("StationID?");
            value = Console.ReadLine();
            stationid = string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);

            if ((!string.IsNullOrEmpty(stationurl)) || stationid > 0) StationDataReader.GetStationData(stationurl, stationid);
        }
    }
}
