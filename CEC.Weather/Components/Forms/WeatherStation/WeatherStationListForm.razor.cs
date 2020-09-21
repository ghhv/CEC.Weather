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
    public partial class WeatherStationListForm : ListComponentBase<DbWeatherStation, WeatherForecastDbContext>
    {
        /// <summary>
        /// The Injected Controller service for this record
        /// </summary>
        [Inject]
        protected WeatherStationControllerService ControllerService { get; set; }


        protected async override Task OnInitializedAsync()
        {
            this.UIOptions.MaxColumn = 2;
            this.Service = this.ControllerService;
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// Method called when the user clicks on a row in the viewer.
        /// </summary>
        /// <param name="id"></param>
        protected void OnView(int id) => this.OnViewAsync<WeatherStationViewerForm>(id);

        /// <summary>
        /// Method called when the user clicks on a row Edit button.
        /// </summary>
        /// <param name="id"></param>
        protected void OnEdit(int id) => this.OnEditAsync<WeatherStationEditorForm>(id);
    }
}
