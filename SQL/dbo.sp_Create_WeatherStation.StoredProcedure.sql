USE [Weather]
GO
/****** Object:  StoredProcedure [dbo].[sp_Create_WeatherStation]    Script Date: 18/09/2020 16:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shaun Curtis
-- Create date: 16 Sep-2020
-- Description:	Adds a new WeatherStation Record
-- =============================================
CREATE PROCEDURE [dbo].[sp_Create_WeatherStation]
	@ID int output
    ,@Name decimal(4,1)
    ,@Latitude decimal(8,4)
    ,@Longitude decimal(8,4)
    ,@Elevation decimal(8,2)
AS
BEGIN
INSERT INTO dbo.WeatherStation
           (
           [Name]
           ,[Latitude]
           ,[Longitude]
           ,[Elevation]
		   )
     VALUES (
			@Name
           ,@Latitude
           ,@Longitude
           ,@Elevation
		   )

SELECT @ID  = SCOPE_IDENTITY();

END
GO
