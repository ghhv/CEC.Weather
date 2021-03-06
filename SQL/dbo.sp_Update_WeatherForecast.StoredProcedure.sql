USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Update_WeatherForecast]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 10 Aug-2020
-- Description:	Updates a WeatherForecast Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Update_WeatherForecast]
	@ID int
	,@Date smalldatetime
    ,@TemperatureC decimal(4,1)
    ,@Frost bit
    ,@SummaryValue int
    ,@OutlookValue int
    ,@Description varchar(max)
    ,@PostCode varchar(50)
    ,@Detail varchar(max)
AS
BEGIN
UPDATE WeatherForecast
   SET [Date] = @Date
      ,[TemperatureC] = @TemperatureC
      ,[Frost] = @Frost
      ,[SummaryValue] = @SummaryValue
      ,[OutlookValue] = @OutlookValue
      ,[Description] = @Description
      ,[PostCode] = @PostCode
      ,[Detail] = @Detail
 WHERE 
	WeatherForecastID = @ID
END
GO
