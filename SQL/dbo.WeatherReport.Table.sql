USE [Weather]
GO
/****** Object:  Table [dbo].[WeatherReport]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeatherReport](
	[WeatherReportID] [int] IDENTITY(1,1) NOT NULL,
	[WeatherStationID] [int] NOT NULL,
	[Date] [smalldatetime] NULL,
	[TempMax] [decimal](8, 4) NULL,
	[TempMin] [decimal](8, 4) NULL,
	[FrostDays] [int] NULL,
	[Rainfall] [decimal](8, 4) NULL,
	[SunHours] [decimal](8, 2) NULL
) ON [PRIMARY]
GO
