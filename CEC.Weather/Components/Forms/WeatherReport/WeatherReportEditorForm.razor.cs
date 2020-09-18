using CEC.Blazor.Components.BaseForms;
using CEC.Weather.Data;
using CEC.Weather.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEC.Weather.Components
{
    public partial class WeatherReportEditorForm : EditRecordComponentBase<DbWeatherReport, WeatherForecastDbContext>
    {
        [Inject]
        public WeatherReportControllerService ControllerService { get; set; }

        public SortedDictionary<int, string> StationLookupList { get; set; }

        private string CardCSS => this.IsModal ? "m-0" : "";

        protected async override Task OnInitializedAsync()
        {
            // Assign the correct controller service
            this.Service = this.ControllerService;
            // Set the delay on the record load as this is a demo project
            this.DemoLoadDelay = 0;
            await base.OnInitializedAsync();
        }

        protected async override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            StationLookupList = await this.Service.GetLookUpListAsync<DbWeatherStation>();
        }

    }
}
