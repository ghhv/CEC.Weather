using CEC.Blazor.Components.BaseForms;
using CEC.Blazor.Extensions;
using CEC.Weather.Data;
using CEC.Weather.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CEC.Weather.Components
{
    public partial class WeatherReportViewerForm : RecordComponentBase<DbWeatherReport, WeatherForecastDbContext>
    {
        [Inject]
        private WeatherReportControllerService ControllerService { get; set; }

        public override string PageTitle => (this.Service?.Record?.DisplayName ?? string.Empty).Trim();

        protected async override Task OnInitializedAsync()
        {
            this.Service = this.ControllerService;
            // Set the delay on the record load as this is a demo project
            this.DemoLoadDelay = 0;
            await base.OnInitializedAsync();
        }

        protected void NextRecord(int increment) 
        {
            var rec = (this._ID + increment) == 0 ? 1 : this._ID + increment;
            rec = rec > this.Service.BaseRecordCount ? this.Service.BaseRecordCount : rec;
            this.NavManager.NavigateTo($"/{this.Service.RecordConfiguration.RecordName}/View?id={rec}");
        }
    }
}
