﻿using Microsoft.AspNetCore.Components;
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

        /// <summary>
        /// Property referencing the Bootstrap modal instance
        /// </summary>
        private BootstrapModal _BootstrapModal { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.UIOptions.MaxColumn = 2;
            this.Service = this.ControllerService;
            await this.Service.Reset();
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// Method called when the user clicks on a row in the viewer.
        /// </summary>
        /// <param name="id"></param>
        protected async void OnView(int id)
        {
            if (this.UIOptions.UseModalViewer && this._BootstrapModal != null)
            {
                var modalOptions = new BootstrapModalOptions()
                {
                    ModalBodyCSS = "p-0",
                    ModalCSS = "modal-xl",
                    HideHeader = true,
                };
                modalOptions.Parameters.Add("ID", id);
                await this._BootstrapModal.Show<WeatherForecastViewerForm>(modalOptions);
            }
            else this.NavigateTo(new EditorEventArgs(PageExitType.ExitToView, id, "WeatherReport"));
        }

        /// <summary>
        /// Method called when the user clicks on a row Edit button.
        /// </summary>
        /// <param name="id"></param>
        protected async void OnEdit(int id)
        {
            if (this.UIOptions.UseModalEditor && this._BootstrapModal != null)
            {
                var modalOptions = new BootstrapModalOptions()
                {
                    ModalBodyCSS = "p-0",
                    ModalCSS = "modal-xl",
                    HideHeader = true
                };
                modalOptions.Parameters.Add("ID", id);
                await this._BootstrapModal.Show<WeatherForecastEditorForm>(modalOptions);
            }
            else this.NavigateTo(new EditorEventArgs(PageExitType.ExitToEditor, id, "WeatherReport"));
        }

    }
}
