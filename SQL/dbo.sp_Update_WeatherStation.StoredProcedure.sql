USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Update_WeatherStation]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 16 Sep-2020
-- Description:	Updates a WeatherStation Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Update_WeatherStation]
	@ID int
    ,@Name decimal(4,1)
    ,@Latitude decimal(8,4)
    ,@Longitude decimal(8,4)
    ,@Elevation decimal(8,2)
AS
BEGIN
UPDATE dbo.WeatherStation
	SET 
           [Name] = @Name
           ,[Latitude] = @Latitude
           ,[Longitude] = @Longitude
           ,[Elevation] = @Elevation
WHERE @ID  = WeatherStationID
END
GO
