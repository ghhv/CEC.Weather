using CEC.Blazor.Components.FormControls;
using CEC.Blazor.Services;
using CEC.Weather.Data;
using CEC.Weather.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CEC.Weather.Components
{
    public partial class MonthYearIDListFilter : ComponentBase
    {
        [Inject]
        private WeatherReportControllerService Service { get; set; }

        [Parameter]
        public EventCallback<bool> FilterUpdated { get; set; }

        [Parameter]
        public bool ShowID { get; set; } = true;

        [Parameter]
        public bool ShowAll { get; set; } = true;

        private SortedDictionary<int, string> MonthLookupList { get; set; }

        private SortedDictionary<int, string> YearLookupList { get; set; }

        private SortedDictionary<int, string> IdLookupList { get; set; }

        private EditContext EditContext => new EditContext(this.Service.Record);

        private int OldMonth = 0;
        private int OldYear = 0;
        private long OldID = 0;

        private int Month
        {
            get => this.Service.FilterList.TryGetFilter("Month", out object value) ? (int)value : 0;
            set
            {
                this.Service.FilterList.SetFilter("Month", value);
                if (this.Month != this.OldMonth)
                {
                    this.OldMonth = this.Month;
                    this.Service.TriggerFilterChangedEvent(this);
                }
            }
        }

        private int Year
        {
            get => this.Service.FilterList.TryGetFilter("Year", out object value) ? (int)value : 0;
            set
            {
                this.Service.FilterList.SetFilter("Year", value);
                if (this.Year != this.OldYear)
                {
                    this.OldYear = this.Year;
                    this.Service.TriggerFilterChangedEvent(this);
                }
            }
        }

        private int ID
        {
            get => this.Service.FilterList.TryGetFilter("WeatherStationID", out object value) ? (int)value : 0;
            set
            {
                this.Service.FilterList.SetFilter("WeatherStationID", value);
                if (this.ID != this.OldID)
                {
                    this.OldID = this.ID;
                    this.Service.TriggerFilterChangedEvent(this);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            this.IdLookupList = await Service.GetLookUpListAsync<DbWeatherStation>();
            this.MonthLookupList = new SortedDictionary<int, string>();
            this.MonthLookupList.Add(0, "-- ALL --");
            for (int i = 1; i < 13; i++) this.MonthLookupList.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
            this.YearLookupList = new SortedDictionary<int, string>();
            this.YearLookupList.Add(0, "-- ALL --");
            for (var year = 1928; year <= DateTime.Now.Year; year++) this.YearLookupList.Add(year, year.ToString());
            this.OldYear = this.Year;
            this.OldMonth = this.Month;
        }
    }
}
