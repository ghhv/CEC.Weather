USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Add_WeatherReport]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 16 Sep-2020
-- Description:	Adds a new WeatherReport Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Add_WeatherReport]
	@ID int output
    ,@WeatherStationID int
	,@Year int
	,@Month int 
    ,@TempMax decimal(8,4)
    ,@TempMin decimal(8,4)
    ,@FrostDays int
    ,@Rainfall decimal(8,4)
    ,@SunHours decimal(8,2)
AS
BEGIN
DECLARE @Date smalldatetime = DATEFROMPARTS(@Year, @Month, 1)

SELECT @ID = MAX(R.ID) FROM vw_WeatherReport R 
WHERE R.Month = @Month AND R.Year = @Year AND R.WeatherStationID = @WeatherStationID 

SELECT @ID 

IF (@ID IS NULL)
BEGIN
	EXEC sp_Create_WeatherReport @ID OUTPUT, @WeatherStationID = @WeatherStationID, @Date = @Date, @TempMax = @TempMax, @TempMin = @TempMin, @FrostDays = @FrostDays, @Rainfall = @Rainfall, @SunHours = @SunHours
END
ELSE
BEGIN
	EXEC sp_Update_WeatherReport @ID =@ID, @WeatherStationID = @WeatherStationID, @Date = @Date, @TempMax = @TempMax, @TempMin = @TempMin, @FrostDays = @FrostDays, @Rainfall = @Rainfall, @SunHours = @SunHours
END

END
GO
