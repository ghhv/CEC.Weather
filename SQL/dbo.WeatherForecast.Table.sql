USE [Weather]
GO
/****** Object:  Table [dbo].[WeatherForecast]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeatherForecast](
	[WeatherForecastID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[TemperatureC] [decimal](4, 1) NOT NULL,
	[Frost] [bit] NOT NULL,
	[SummaryValue] [int] NOT NULL,
	[OutlookValue] [int] NOT NULL,
	[Description] [varchar](max) NULL,
	[PostCode] [varchar](50) NULL,
	[Detail] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[WeatherForecast] ADD  CONSTRAINT [DF_WeatherForecast_Frost]  DEFAULT ((0)) FOR [Frost]
GO
