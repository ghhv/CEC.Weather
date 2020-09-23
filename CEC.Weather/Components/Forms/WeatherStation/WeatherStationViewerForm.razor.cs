using CEC.Blazor.Components.BaseForms;
using CEC.Weather.Data;
using CEC.Weather.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CEC.Weather.Components
{
    public partial class WeatherStationViewerForm : RecordComponentBase<DbWeatherStation, WeatherForecastDbContext>
    {
        [Inject]
        private WeatherStationControllerService ControllerService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Service = this.ControllerService;
            // Set the delay on the record load as this is a demo project
            this.DemoLoadDelay = 0;
            await base.OnInitializedAsync();
        }
    }
}
