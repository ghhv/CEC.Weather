USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Create_WeatherForecast]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 10 Aug-2020
-- Description:	Adds a new WeatherForecast Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Create_WeatherForecast]
	@ID int output
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
INSERT INTO [dbo].[WeatherForecast]
           ([Date]
           ,[TemperatureC]
           ,[Frost]
           ,[SummaryValue]
           ,[OutlookValue]
           ,[Description]
           ,[PostCode]
           ,[Detail])
     VALUES (
			@Date
           ,@TemperatureC
           ,@Frost
           ,@SummaryValue
           ,@OutlookValue
           ,@Description
           ,@PostCode
           ,@Detail
		   )

SELECT @ID  = SCOPE_IDENTITY();

END
GO
