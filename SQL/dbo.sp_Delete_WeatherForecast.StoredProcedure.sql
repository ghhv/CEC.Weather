USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_WeatherForecast]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 10 Aug-2020
-- Description:	Deletes a WeatherForecast Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Delete_WeatherForecast]
	@ID int
AS
BEGIN
DELETE FROM 
	WeatherForecast
 WHERE 
	WeatherForecastID = @ID
END
GO
