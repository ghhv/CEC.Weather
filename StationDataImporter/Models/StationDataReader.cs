using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Data;

namespace WeatherApp.Models
{
    class StationDataReader
    {
        public const string StationRegEx = @"(\d+)\D*(\d+)\D*([-\.\d]+)\D*([-\.\d]+)\D*([-\.\d]+)";
        
        public const string StationDataRegEx = @"([-\d]+)[^\d]*([-\d]+)[^\d-]*([-\.\d]+)[^\d-]*([-\.\d]+)[^\d-]*([-\.\d]+)[^\d-]*([-\.\d]+)[^\d-]*([-\.\d]+)";

        public static Task<bool> GetStationData(string Url, int stationid)
        {
            var station = new WeatherStation() { GUID = Guid.NewGuid().ToString(), SourceUrl = Url };
            var stationData = new WeatherStationDataSet() { GUID = station.GUID };
            var isdata = false;
            WebClient client = new WebClient();
            var data = client.DownloadString(new System.Uri(Url));
            using (StringReader reader = new StringReader(data))
            {
                string line;
                int lineno = 1;
                Regex rxStation = new Regex(StationRegEx);
                Regex rxData = new Regex(StationDataRegEx);
                while (null != (line = reader.ReadLine()))
                {
                    if (lineno == 1) station.Station = line;
                    if (lineno == 2)
                    {
                        var match = rxStation.Match(line);
                        if (match.Groups.Count == 6 )
                        {
                            station.GridData = GridReference.GetGridReference(match.Groups[1].Value, match.Groups[2].Value);
                            station.Location = GPSLocation.GetGPSLocation(match.Groups[3].Value, match.Groups[4].Value, match.Groups[5].Value);
                        }
                    }
                    if (lineno > 7)
                    {
                        var match = rxData.Match(line);
                        if (match.Groups.Count == 8)
                        {
                            stationData.Data.Add(WeatherStationData.GetWeatherStationData(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value, match.Groups[5].Value, match.Groups[6].Value, match.Groups[7].Value));
                        }
                    }
                    lineno++;
                }
            }
            foreach (var rec in stationData.Data)
            {
                Console.WriteLine($"Added: {rec.Month}: {rec.Year}");
                WriteData(rec, stationid);
            }
            return Task.FromResult(isdata);
        }

        public string GetHeader(string data)
        {
            return string.Empty;
        }

        public static int WriteData(WeatherStationData data, int StationID)
        {
            int id = 0;
            using (SqlConnection sql = new SqlConnection("Server = (localdb)\\mssqllocaldb; Database = Weather; Trusted_Connection = True; MultipleActiveResultSets = true"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Add_WeatherReport", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@WeatherStationID", StationID);
                    cmd.Parameters.AddWithValue("@Year", data.Year);
                    cmd.Parameters.AddWithValue("@Month", data.Month);
                    cmd.Parameters.AddWithValue("@TempMax", data.TMax);
                    cmd.Parameters.AddWithValue("@TempMin", data.TMin);
                    cmd.Parameters.AddWithValue("@FrostDays", data.FrostDays);
                    cmd.Parameters.AddWithValue("@Rainfall", data.Rainfall);
                    cmd.Parameters.AddWithValue("@SunHours", data.Sunshine);
                    if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return id;
        }
    }
}
