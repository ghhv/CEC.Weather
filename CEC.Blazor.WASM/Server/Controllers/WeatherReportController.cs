using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC = Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CEC.Weather.Services;
using CEC.Weather.Data;
using CEC.Blazor.Data;
using CEC.Blazor.Components;

namespace CEC.Blazor.WASM.Server.Controllers
{
    [ApiController]
    public class WeatherReportController : ControllerBase
    {
        protected IWeatherReportDataService DataService { get; set; }

        private readonly ILogger<WeatherReportController> logger;

        public WeatherReportController(ILogger<WeatherReportController> logger, IWeatherReportDataService dataService)
        {
            this.DataService = dataService;
            this.logger = logger;
        }

        [MVC.Route("weatherreport/list")]
        [HttpGet]
        public async Task<List<DbWeatherReport>> GetList() => await DataService.GetRecordListAsync();

        [MVC.Route("weatherreport/filteredlist")]
        [HttpPost]
        public async Task<List<DbWeatherReport>> GetFilteredRecordListAsync([FromBody] FilterList filterList) => await DataService.GetFilteredRecordListAsync(filterList);

        [MVC.Route("weatherreport/distinctlist")]
        [HttpPost]
        public async Task<List<string>> GetDistinctListAsync([FromBody] DbDistinctRequest req) => await DataService.GetDistinctListAsync(req);

        [MVC.Route("weatherreport/base")]
        public async Task<List<DbBaseRecord>> GetBaseAsync() => await DataService.GetBaseRecordListAsync<DbWeatherReport>();

        [MVC.Route("weatherreport/count")]
        [HttpGet]
        public async Task<int> Count() => await DataService.GetRecordListCountAsync();

        [MVC.Route("weatherreport/get")]
        [HttpGet]
        public async Task<DbWeatherReport> GetRec(int id) => await DataService.GetRecordAsync(id);

        [MVC.Route("weatherreport/read")]
        [HttpPost]
        public async Task<DbWeatherReport> Read([FromBody]int id) => await DataService.GetRecordAsync(id);

        [MVC.Route("weatherreport/update")]
        [HttpPost]
        public async Task<DbTaskResult> Update([FromBody]DbWeatherReport record) => await DataService.UpdateRecordAsync(record);

        [MVC.Route("weatherreport/create")]
        [HttpPost]
        public async Task<DbTaskResult> Create([FromBody]DbWeatherReport record) => await DataService.CreateRecordAsync(record);

        [MVC.Route("weatherreport/delete")]
        [HttpPost]
        public async Task<DbTaskResult> Delete([FromBody] DbWeatherReport record) => await DataService.DeleteRecordAsync(record);
    }
}
