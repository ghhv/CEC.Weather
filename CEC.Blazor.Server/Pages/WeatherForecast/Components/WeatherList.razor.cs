﻿using Microsoft.AspNetCore.Components;
using CEC.Blazor.Server.Data;
using CEC.Blazor.Components.Base;
using CEC.Blazor.Server.Services;
using System.Threading.Tasks;
using CEC.Blazor.Components;
using CEC.Blazor.Components.UIControls;
using CEC.Blazor.Components.Modal;
using System;
using CEC.Blazor.Data;

namespace CEC.Blazor.Server.Pages.Components
{
    public partial class WeatherList : ListComponentBase<WeatherForecast>
    {
        /// <summary>
        /// The Injected Controller service for this record
        /// </summary>
        [Inject]
        protected WeatherForecastControllerService ControllerService { get; set; }

        /// <summary>
        /// Property referencing the Bootstrap modal instance
        /// </summary>
        private BootstrapModal _BootstrapModal { get; set; }

        protected override void OnInitialized()
        {
            this.UIOptions.MaxColumn = 3;
            this.Service = this.ControllerService;
            base.OnInitialized();
        }

        /// <summary>
        /// Overrides the base method to load the sorted version of the list getter
        /// and set the default sort column
        /// </summary>
        /// <param name="withDelegate"></param>
        protected async override Task LoadPagingAsync(bool withDelegate = true)
        {
            await base.LoadPagingAsync(false);
            this.Paging.PageLoaderAsync = new PagingData<WeatherForecast>.PageLoaderDelegateAsync(this.ControllerService.GetDataPageWithSortingAsync);
            this.Paging.DefaultSortColumn = "WeatherForecastID";
            await this.Paging.LoadAsync();
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
                await this._BootstrapModal.Show<WeatherViewer>(modalOptions);
            }
            else this.NavigateTo(new EditorEventArgs(PageExitType.ExitToView, id, "WeatherForecast"));
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
                await this._BootstrapModal.Show<WeatherEditor>(modalOptions);
            }
            else this.NavigateTo(new EditorEventArgs(PageExitType.ExitToEditor, id, "WeatherForecast"));
        }

    }
}