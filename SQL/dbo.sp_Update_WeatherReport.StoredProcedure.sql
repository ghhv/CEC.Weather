USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Update_WeatherReport]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 16 Sep-2020
-- Description:	Updates a WeatherReport Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Update_WeatherReport]
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
GO
