using CEC.Blazor.Data;
using CEC.Weather.Data;
using CEC.Blazor.Services;
using Microsoft.Extensions.Configuration;

namespace CEC.Weather.Services
{
    public class WeatherStationServerDataService :
        BaseServerDataService<DbWeatherStation, WeatherForecastDbContext>,
        IWeatherStationDataService
    {
        public WeatherStationServerDataService(IConfiguration configuration, IDbContextFactory<WeatherForecastDbContext> dbcontext) : base(configuration, dbcontext)
        {
            this.RecordConfiguration = new RecordConfigurationData() { RecordName = "WeatherStation", RecordDescription = "Weather Station", RecordListName = "WeatherStations", RecordListDecription = "Weather Stations" };
        }
    }
}
