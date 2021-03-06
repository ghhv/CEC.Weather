USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_WeatherReport]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 16 Sep-2020
-- Description:	Deletes a WeatherRecord
-- =============================================
CREATE PROCEDURE [dbo].[sp_Delete_WeatherReport]
	@ID int
AS
BEGIN
DELETE FROM WeatherReport
WHERE @ID  = WeatherReportID
END
GO
