USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Create_WeatherReport]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 16 Sep-2020
-- Description:	Adds a new WeatherReport Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Create_WeatherReport]
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
GO
