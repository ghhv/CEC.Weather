USE [Weather]
GO
/****** Object:  Table [dbo].[WeatherStation]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeatherStation](
	[WeatherStationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Latitude] [decimal](8, 4) NOT NULL,
	[Longitude] [decimal](8, 4) NOT NULL,
	[Elevation] [decimal](8, 2) NOT NULL
) ON [PRIMARY]
GO
