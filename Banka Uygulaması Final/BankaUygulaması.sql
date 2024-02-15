USE [master]
GO
/****** Object:  Database [Banka]    Script Date: 1/12/2024 11:39:44 PM ******/
CREATE DATABASE [Banka]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Banka', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Banka.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Banka_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Banka_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Banka] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Banka].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Banka] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Banka] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Banka] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Banka] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Banka] SET ARITHABORT OFF 
GO
ALTER DATABASE [Banka] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Banka] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Banka] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Banka] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Banka] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Banka] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Banka] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Banka] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Banka] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Banka] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Banka] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Banka] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Banka] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Banka] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Banka] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Banka] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Banka] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Banka] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Banka] SET  MULTI_USER 
GO
ALTER DATABASE [Banka] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Banka] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Banka] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Banka] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Banka] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Banka] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Banka] SET QUERY_STORE = ON
GO
ALTER DATABASE [Banka] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Banka]
GO
/****** Object:  Table [dbo].[Girdiler]    Script Date: 1/12/2024 11:39:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Girdiler](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TC] [varchar](50) NOT NULL,
	[Ad] [varchar](50) NOT NULL,
	[Soyad] [varchar](50) NOT NULL,
	[IBAN] [varchar](50) NULL,
	[Para] [varchar](50) NULL,
	[Şifre] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Girdiler__3214EC27E3044CBA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Girdiler] ON 

INSERT [dbo].[Girdiler] ([ID], [TC], [Ad], [Soyad], [IBAN], [Para], [Şifre]) VALUES (1, N'19580251146', N'furkan', N'çakan', N'TR 9916901749295394490376', N'2442', N'577875')
SET IDENTITY_INSERT [dbo].[Girdiler] OFF
GO
USE [master]
GO
ALTER DATABASE [Banka] SET  READ_WRITE 
GO
