﻿using CEC.Blazor.Components.BaseForms;
using CEC.Weather.Data;
using CEC.Weather.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CEC.Weather.Components
{
    public partial class WeatherStationEditorForm : EditRecordComponentBase<DbWeatherStation, WeatherForecastDbContext>
    {
        [Inject]
        public WeatherStationControllerService ControllerService { get; set; }

        private string CardCSS => this.IsModal ? "m-0" : "";

        protected async override Task OnInitializedAsync()
        {
            // Assign the correct controller service
            this.Service = this.ControllerService;
            // Set the delay on the record load as this is a demo project
            this.DemoLoadDelay = 0;
            await base.OnInitializedAsync();
        }
    }
}
