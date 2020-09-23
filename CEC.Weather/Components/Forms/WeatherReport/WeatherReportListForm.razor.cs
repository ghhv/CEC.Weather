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
        }

        /// <summary>
        /// inherited - loads the filter
        /// </summary>
        protected override void LoadFilter()
        {
            // Potentially hug data set so we only show filtered results
            this.OnlyLoadIfFilter = true;
            ((CEC.Blazor.Services.IControllerPagingService<DbWeatherReport>)this.Service).DefaultSortColumn = "Date";
            // Before the call to base so the filter is set before the get the list
            if (this.IsService &&  this.WeatherStationID > 0)
            {
                this.Service.FilterList.Filters.Clear();
                this.Service.FilterList.Filters.Add("WeatherStationID", this.WeatherStationID);
            }
            base.LoadFilter();
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
