using CEC.Weather.Data;
using CEC.Blazor.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using CEC.Blazor.Data;

namespace CEC.Weather.Services
{
    public class WeatherReportWASMDataService :
        BaseWASMDataService<DbWeatherReport, WeatherForecastDbContext>,
        IWeatherReportDataService
    {
        public WeatherReportWASMDataService(IConfiguration configuration, HttpClient httpClient) : base(configuration, httpClient)
        {
            this.RecordConfiguration = new RecordConfigurationData() { RecordName = "WeatherReport", RecordDescription = "Weather Report", RecordListName = "WeatherReports", RecordListDecription = "Weather Reports" };
        }
    }
}
