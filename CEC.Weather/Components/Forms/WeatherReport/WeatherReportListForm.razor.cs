using Microsoft.AspNetCore.Components;
using CEC.Blazor.Components.BaseForms;
using CEC.Weather.Data;
using CEC.Weather.Services;
using System.Threading.Tasks;
using CEC.Blazor.Components;
using CEC.Blazor.Components.UIControls;
using CEC.Blazor.Components.Modal;

namespace CEC.Weather.Components
{
    public partial class WeatherReportListForm : ListComponentBase<DbWeatherReport, WeatherForecastDbContext>
    {
        /// <summary>
        /// The Injected Controller service for this record
        /// </summary>
        [Inject]
        protected WeatherReportControllerService ControllerService { get; set; }

        [Parameter]
        public int WeatherStationID { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.UIOptions.MaxColumn = 2;
            this.Service = this.ControllerService;
            await base.OnInitializedAsync();
            // add after the base as base resets the service and the filter
            if (this.WeatherStationID > 0)
            {
                this.Service.FilterList.Filters.Clear();
                this.Service.FilterList.Filters.Add("WeatherStationID", this.WeatherStationID);
            }
        }

        /// <summary>
        /// Method called when the user clicks on a row in the viewer.
        /// </summary>
        /// <param name="id"></param>
        protected void OnView(int id) => this.OnViewAsync<WeatherReportViewerForm>(id);

        /// <summary>
        /// Method called when the user clicks on a row Edit button.
        /// </summary>
        /// <param name="id"></param>
        protected void OnEdit(int id) => this.OnEditAsync<WeatherReportEditorForm>(id);

    }
}
