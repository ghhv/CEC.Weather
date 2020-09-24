using System.Collections.Generic;
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
    public class WeatherStationController : ControllerBase
    {
        protected IWeatherStationDataService DataService { get; set; }

        private readonly ILogger<WeatherStationController> logger;

        public WeatherStationController(ILogger<WeatherStationController> logger, IWeatherStationDataService dataService)
        {
            this.DataService = dataService;
            this.logger = logger;
        }

        [MVC.Route("weatherstation/list")]
        [HttpGet]
        public async Task<List<DbWeatherStation>> GetList() => await DataService.GetRecordListAsync();

        [MVC.Route("weatherStation/filteredlist")]
        [HttpPost]
        public async Task<List<DbWeatherStation>> GetFilteredRecordListAsync([FromBody] FilterList filterList) => await DataService.GetFilteredRecordListAsync(filterList);

        [MVC.Route("weatherreport/distinctlist")]
        [HttpPost]
        public async Task<List<string>> GetDistinctListAsync([FromBody] DbDistinctRequest req) => await DataService.GetDistinctListAsync(req);

        [MVC.Route("weatherstation/base")]
        public async Task<List<DbBaseRecord>> GetBaseAsync() => await DataService.GetBaseRecordListAsync<DbWeatherStation>();

        [MVC.Route("weatherstation/count")]
        [HttpGet]
        public async Task<int> Count() => await DataService.GetRecordListCountAsync();

        [MVC.Route("weatherstation/get")]
        [HttpGet]
        public async Task<DbWeatherStation> GetRec(int id) => await DataService.GetRecordAsync(id);

        [MVC.Route("weatherstation/read")]
        [HttpPost]
        public async Task<DbWeatherStation> Read([FromBody]int id) => await DataService.GetRecordAsync(id);

        [MVC.Route("weatherstation/update")]
        [HttpPost]
        public async Task<DbTaskResult> Update([FromBody]DbWeatherStation record) => await DataService.UpdateRecordAsync(record);

        [MVC.Route("weatherstation/create")]
        [HttpPost]
        public async Task<DbTaskResult> Create([FromBody]DbWeatherStation record) => await DataService.CreateRecordAsync(record);

        [MVC.Route("weatherstation/delete")]
        [HttpPost]
        public async Task<DbTaskResult> Delete([FromBody] DbWeatherStation record) => await DataService.DeleteRecordAsync(record);
    }
}
