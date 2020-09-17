# Buildinging a Database Appication in Blazor 
## Part 5 - Building

This article walks through adding new records to the appliction.

### Sample Project and Code

See the [CEC.Blazor GitHub Repository](https://github.com/ShaunCurtis/CEC.Blazor) for the libraries and sample projects.

### Overview of the Process

1. Database
   1. Add Tables
   2. Add Views
   3. Add CUD Stored Procedures
2. Update *WeatherForecastDBContext* in *CEC.Weather* with the new DbSets and update *OnModelCreating()* with views
3. Create *IWeatherStationDataService*
4. Create *WeatherStationServerDataService* and *WeatherStationWASMDataService*
5. Create *WeatherStationControllerService*
6. Add new services to *ServiceCollectionExtensions.AddApplicationServices* in Server and WASM projects

### Database

Add the new tables to the database
```sql
CREATE TABLE [dbo].[WeatherStation](
	[WeatherStationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Latitude] [decimal](8, 4) NOT NULL,
	[Longitude] [decimal](8, 4) NOT NULL,
	[Elevation] [decimal](8, 2) NOT NULL
)
```
```sql
CREATE TABLE [dbo].[WeatherReport](
	[WeatherReportID] [int] IDENTITY(1,1) NOT NULL,
	[WeatherStationID] [int] NOT NULL,
	[Date] [smalldatetime] NULL,
	[TempMax] [decimal](8, 4) NULL,
	[TempMin] [decimal](8, 4) NULL,
	[FrostDays] [int] NULL,
	[Rainfall] [decimal](8, 4) NULL,
	[SunHours] [decimal](8, 2) NULL
)
```

Add the two views
```sql
CREATE VIEW vw_WeatherStation
AS
SELECT        
    WeatherStationID AS ID, 
    Name, 
    Latitude, 
    Longitude, 
    Elevation, 
    Name AS DisplayName
FROM WeatherStation
```
```sql
CREATE VIEW vw_WeatherReport
AS
SELECT        
    R.WeatherReportID as ID, 
    R.WeatherStationID, 
    R.TempMax, 
    R.TempMin, 
    R.FrostDays, 
    R.Rainfall, 
    R.SunHours, 
    S.Name AS WeatherStationName, 
    'Report For ' + CONVERT(VARCHAR(50), Date, 106) AS DisplayName
FROM  WeatherReport AS R 
LEFT INNER JOIN dbo.WeatherStation AS S ON R.WeatherStationID = S.WeatherStationID
```

Add the Stored Procedures 
```sql
CREATE PROCEDURE sp_Create_WeatherStation
	@ID int output
    ,@Name decimal(4,1)
    ,@Latitude decimal(8,4)
    ,@Longitude decimal(8,4)
    ,@Elevation decimal(8,2)
AS
BEGIN
INSERT INTO dbo.WeatherStation
           ([Name]
           ,[Latitude]
           ,[Longitude]
           ,[Elevation])
     VALUES (@Name
           ,@Latitude
           ,@Longitude
           ,@Elevation)
SELECT @ID  = SCOPE_IDENTITY();
END
```
```sql
CREATE PROCEDURE sp_Update_WeatherStation
	@ID int
    ,@Name decimal(4,1)
    ,@Latitude decimal(8,4)
    ,@Longitude decimal(8,4)
    ,@Elevation decimal(8,2)
AS
BEGIN
UPDATE dbo.WeatherStation
	SET 
           [Name] = @Name
           ,[Latitude] = @Latitude
           ,[Longitude] = @Longitude
           ,[Elevation] = @Elevation
WHERE @ID  = WeatherStationID
END
```
```sql
CREATE PROCEDURE sp_Delete_WeatherStation
	@ID int
AS
BEGIN
DELETE FROM WeatherStation
WHERE @ID  = WeatherStationID
END
```
```sql
CREATE PROCEDURE sp_Create_WeatherReport
	@ID int output
    ,@WeatherStationID int
    ,@Date smalldatetime
    ,@TempMax decimal(8,4)
    ,@TempMin decimal(8,4)
    ,@FrostDays int
    ,@Rainfall decimal(8,4)
    ,@SunHours decimal(8,2)
AS
BEGIN
INSERT INTO WeatherReport
           ([WeatherStationID]
           ,[Date]
           ,[TempMax]
           ,[TempMin]
           ,[FrostDays]
           ,[Rainfall]
           ,[SunHours])
     VALUES
           (@WeatherStationID
           ,@Date
           ,@TempMax
           ,@TempMin
           ,@FrostDays
           ,@Rainfall
           ,@SunHours)
SELECT @ID  = SCOPE_IDENTITY();
END
```
```sql
CREATE PROCEDURE sp_Update_WeatherReport
	@ID int output
    ,@WeatherStationID int
    ,@Date smalldatetime
    ,@TempMax decimal(8,4)
    ,@TempMin decimal(8,4)
    ,@FrostDays int
    ,@Rainfall decimal(8,4)
    ,@SunHours decimal(8,2)
AS
BEGIN
UPDATE WeatherReport
   SET [WeatherStationID] = @WeatherStationID
      ,[Date] = @Date
      ,[TempMax] = @TempMax
      ,[TempMin] = @TempMin
      ,[FrostDays] = @FrostDays
      ,[Rainfall] = @Rainfall
      ,[SunHours] = @SunHours
WHERE @ID  = WeatherReportID
END
```
```sql
CREATE PROCEDURE sp_Delete_WeatherReport
	@ID int
AS
BEGIN
DELETE FROM WeatherReport
WHERE @ID  = WeatherReportID
END
```
### Update WeatherForecastDbContext

Add two new *DbSet* properties to the class and two *modelBuilder* calls to *OnModelCreating*. 
```c#
// CEC.Weather/Data/WeatherForecastDbContext.cs
......
public DbSet<DbWeatherStation> WeatherStation { get; set; }

public DbSet<DbWeatherReport> WeatherReport { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
......
    modelBuilder
        .Entity<DbWeatherStation>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("vw_WeatherStation");
        });
    modelBuilder
        .Entity<DbWeatherReport>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("vw_WeatherReport");
        });
}
```

### Add Services

```c#
// CEC.Weather/Services/Interfaces/IWeatherStationDataService.cs
using CEC.Blazor.Services;
using CEC.Weather.Data;

namespace CEC.Weather.Services
{
    public interface IWeatherStationDataService : 
        IDataService<DbWeatherStation, WeatherForecastDbContext>
    {}
}
```
```c#
// CEC.Weather/Services/DataServices/WeatherStationServerDataService.cs
using CEC.Blazor.Data;
using CEC.Weather.Data;
using CEC.Blazor.Services;
using Microsoft.Extensions.Configuration;

namespace CEC.Weather.Services
{
    public class WeatherStationServerDataService :
        BaseServerDataService<DbWeatherStation, WeatherForecastDbContext>,
        IWeatherStationDataService
    {
        public WeatherStationServerDataService(IConfiguration configuration, IDbContextFactory<WeatherForecastDbContext> dbcontext) : base(configuration, dbcontext)
        {
            this.RecordConfiguration = new RecordConfigurationData() { RecordName = "WeatherStation", RecordDescription = "Weather Station", RecordListName = "WeatherStations", RecordListDecription = "Weather Stations" };
        }
    }
}
```
```c#
// CEC.Weather/Services/DataServices/WeatherStationWASMDataService.cs
using CEC.Weather.Data;
using CEC.Blazor.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using CEC.Blazor.Data;

namespace CEC.Weather.Services
{
    public class WeatherStationWASMDataService :
        BaseWASMDataService<DbWeatherStation, WeatherForecastDbContext>,
        IWeatherStationDataService
    {
        public WeatherStationWASMDataService(IConfiguration configuration, HttpClient httpClient) : base(configuration, httpClient)
        {
            this.RecordConfiguration = new RecordConfigurationData() { RecordName = "WeatherStation", RecordDescription = "Weather Station", RecordListName = "WeatherStations", RecordListDecription = "Weather Stations" };
        }
    }
}
```
```c#
// CEC.Weather/Services/ControllerServices/WeatherStationControllerService.cs
using CEC.Weather.Data;
using CEC.Blazor.Services;
using CEC.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CEC.Weather.Services
{
    public class WeatherStationControllerService : BaseControllerService<DbWeatherStation, WeatherForecastDbContext>, IControllerService<DbWeatherStation, WeatherForecastDbContext>
    {
        /// <summary>
        /// List of Outlooks for Select Controls
        /// </summary>
        public SortedDictionary<int, string> OutlookOptionList => Utils.GetEnumList<WeatherOutlook>();

        public WeatherStationControllerService(NavigationManager navmanager, IConfiguration appconfiguration, IWeatherStationDataService DataService) : base(appconfiguration, navmanager)
        {
            this.Service = DataService;
            this.DefaultSortColumn = "ID";
        }
    }
}
```







```c#
```

### WeatherStationListForm

```c#
// CEC.Weather/Components/Forms/WeatherStation/WeatherStationListForm.razor.cs
@using CEC.Blazor.Components
@using CEC.Blazor.Components.BaseForms
@using CEC.Blazor.Components.UIControls
@using CEC.Weather.Data

@namespace CEC.Weather.Components

@inherits ListComponentBase<DbWeatherStation, WeatherForecastDbContext>

<UIWrapper UIOptions="@this.UIOptions" RecordConfiguration="@this.Service.RecordConfiguration" OnView="@OnView" OnEdit="@OnEdit">

    <UICardGrid TRecord="DbWeatherStation" IsCollapsible="true" Paging="this.Paging" IsLoading="this.Loading">

        <Title>
            @this.ListTitle
        </Title>

        <TableHeader>

            <UIGridTableHeaderColumn TRecord="DbWeatherStation" Column="1" FieldName="ID">ID</UIGridTableHeaderColumn>
            <UIGridTableHeaderColumn TRecord="DbWeatherStation" Column="2" FieldName="Name">Name</UIGridTableHeaderColumn>
            <UIGridTableHeaderColumn TRecord="DbWeatherStation" Column="3" FieldName="Latitude">Latitiude</UIGridTableHeaderColumn>
            <UIGridTableHeaderColumn TRecord="DbWeatherStation" Column="4" FieldName="Longitude">Longitude</UIGridTableHeaderColumn>
            <UIGridTableHeaderColumn TRecord="DbWeatherStation" Column="5" FieldName="Elevation">Elevation</UIGridTableHeaderColumn>
            <UIGridTableHeaderColumn TRecord="DbWeatherStation" Column="6"></UIGridTableHeaderColumn>

        </TableHeader>

        <RowTemplate>

            <CascadingValue Name="RecordID" Value="@context.ID">

                <UIGridTableColumn TRecord="DbWeatherStation" Column="1">@context.ID</UIGridTableColumn>
                <UIGridTableColumn TRecord="DbWeatherStation" Column="2">@context.Name</UIGridTableColumn>
                <UIGridTableColumn TRecord="DbWeatherStation" Column="3">@context.Latitude</UIGridTableColumn>
                <UIGridTableColumn TRecord="DbWeatherStation" Column="4">@context.Longitude</UIGridTableColumn>
                <UIGridTableColumn TRecord="DbWeatherStation" Column="5">@context.Elevation</UIGridTableColumn>
                <UIGridTableEditColumn TRecord="DbWeatherStation"></UIGridTableEditColumn>

            </CascadingValue>

        </RowTemplate>

        <Navigation>

            <UIListButtonRow>
                <Paging>
                    <PagingControl TRecord="DbWeatherStation" Paging="this.Paging"></PagingControl>
                </Paging>
            </UIListButtonRow>

        </Navigation>

    </UICardGrid>

</UIWrapper>

<BootstrapModal @ref="this._BootstrapModal"></BootstrapModal>
```

```c#
// CEC.Weather/Components/Forms/WeatherStation/WeatherStationListForm.razor.cs
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
            else this.NavigateTo(new EditorEventArgs(PageExitType.ExitToView, id, "WeatherStation"));
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
            else this.NavigateTo(new EditorEventArgs(PageExitType.ExitToEditor, id, "WeatherStation"));
        }

    }
}
```



### Wrap Up
This article gives an overview on building UI Controls with components, and examines some example components in more detail.

Some key points to note:
1. Using this methodology, you have control over the HTML and Css markup.
2. You can use as little or as much abstraction as you wish.

