USE [master]
GO
/****** Object:  Database [Weather]    Script Date: 18/09/2020 16:51:49 ******/
CREATE DATABASE [Weather]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WeatherForecast', FILENAME = N'C:\Users\Shaun.Obsidian\WeatherForecast.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WeatherForecast_log', FILENAME = N'C:\Users\Shaun.Obsidian\WeatherForecast_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Weather] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Weather].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Weather] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Weather] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Weather] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Weather] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Weather] SET ARITHABORT OFF 
GO
ALTER DATABASE [Weather] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Weather] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Weather] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Weather] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Weather] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Weather] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Weather] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Weather] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Weather] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Weather] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Weather] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Weather] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Weather] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Weather] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Weather] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Weather] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Weather] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Weather] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Weather] SET  MULTI_USER 
GO
ALTER DATABASE [Weather] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Weather] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Weather] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Weather] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Weather] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Weather] SET QUERY_STORE = OFF
GO
USE [Weather]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE [Weather] SET  READ_WRITE 
GO
