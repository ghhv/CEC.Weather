using CEC.Blazor.Data;
using CEC.Blazor.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CEC.Weather.Data
{
    /// <summary>
    /// Data Record for a Weather Foreecast
    /// Data validation is handled by the Fluent Validator
    /// Custom Attributes are for building the EF strored procedures
    /// </summary>
    public class DbWeatherReport :IDbRecord<DbWeatherReport>
    {
        [NotMapped]
        public int WeatherReportID { get => this.ID; }

        [SPParameter(IsID = true, DataType = SqlDbType.Int)]
        public int ID { get; set; } = -1;

        [SPParameter(DataType = SqlDbType.Int)]
        public decimal WeatherStationID { get; set; } = -1;

        [SPParameter(DataType = SqlDbType.SmallDateTime)]
        public DateTime Date { get; set; } = DateTime.Now.Date;

        [SPParameter(DataType = SqlDbType.Decimal)]
        public decimal MaxTemp { get; set; } = 1000;

        [SPParameter(DataType = SqlDbType.Decimal)]
        public decimal MinTemp { get; set; } = 1000;

        [SPParameter(DataType = SqlDbType.Int)]
        public decimal FrostDays { get; set; } = -1;

        [SPParameter(DataType = SqlDbType.Decimal)]
        public decimal Rainfall { get; set; } = -1;

        [SPParameter(DataType = SqlDbType.Decimal)]
        public decimal SunHours { get; set; } = -1;

        public string DisplayName { get; set; }

        public string WeatherStationName { get; set; }

        public void SetNew() => this.ID = 0;

        public DbWeatherReport ShadowCopy()
        {
            return new DbWeatherReport() {
                ID = this.ID,
                Date = this.Date,
                MaxTemp = this.MaxTemp,
                MinTemp = this.MinTemp,
                FrostDays = this.FrostDays,
                Rainfall = this.Rainfall,
                SunHours = this.SunHours
            };
        }
    }
}
