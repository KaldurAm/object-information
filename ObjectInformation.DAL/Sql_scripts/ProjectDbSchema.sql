USE [master]
GO
/****** Object:  Database [ObjectInformation]    Script Date: 14.09.2020 14:47:27 ******/
CREATE DATABASE [ObjectInformation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ObjectInformation', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ObjectInformation.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ObjectInformation_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ObjectInformation_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ObjectInformation] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ObjectInformation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ObjectInformation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ObjectInformation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ObjectInformation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ObjectInformation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ObjectInformation] SET ARITHABORT OFF 
GO
ALTER DATABASE [ObjectInformation] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ObjectInformation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ObjectInformation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ObjectInformation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ObjectInformation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ObjectInformation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ObjectInformation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ObjectInformation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ObjectInformation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ObjectInformation] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ObjectInformation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ObjectInformation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ObjectInformation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ObjectInformation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ObjectInformation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ObjectInformation] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ObjectInformation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ObjectInformation] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ObjectInformation] SET  MULTI_USER 
GO
ALTER DATABASE [ObjectInformation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ObjectInformation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ObjectInformation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ObjectInformation] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ObjectInformation] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ObjectInformation] SET QUERY_STORE = OFF
GO
USE [ObjectInformation]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Checklist]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Checklist](
	[ChecklistId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Checklist] PRIMARY KEY CLUSTERED 
(
	[ChecklistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NOT NULL,
	[RegionId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[CommentText] [nvarchar](1000) NOT NULL,
	[ObjectRealtyId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[CommentDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coordinate]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coordinate](
	[CoordinateId] [int] IDENTITY(1,1) NOT NULL,
	[PolygonId] [int] NOT NULL,
	[lat] [float] NOT NULL,
	[lng] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Coordinate] PRIMARY KEY CLUSTERED 
(
	[CoordinateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[CurrencyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[District]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [nvarchar](100) NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.District] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentType]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentTypeName] [nvarchar](200) NOT NULL,
	[ChecklistId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DocumentType] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjectProperty]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectProperty](
	[ObjectPropertyId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
	[ObjectRealtyId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ObjectProperty] PRIMARY KEY CLUSTERED 
(
	[ObjectPropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjectRealty]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectRealty](
	[ObjectRealtyId] [int] IDENTITY(1,1) NOT NULL,
	[ObjectTypeId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Cost] [float] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[CostDate] [datetime] NULL,
	[DateOfSale] [datetime] NULL,
	[CurrencyRate] [float] NOT NULL,
	[Square] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [nvarchar](max) NULL,
	[lat] [nvarchar](max) NULL,
	[lng] [nvarchar](max) NULL,
	[zoom] [nvarchar](max) NULL,
	[CadastralNumber] [nvarchar](max) NULL,
	[CostDCT] [float] NOT NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ObjectRealty] PRIMARY KEY CLUSTERED 
(
	[ObjectRealtyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjectRealtyPledgers]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectRealtyPledgers](
	[ObjectRealtyPledgersId] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ObjectRealtyId] [int] NOT NULL,
	[PledgersId] [int] NOT NULL,
	[PledgeDate] [datetime] NULL,
	[MortgageValue] [float] NOT NULL,
	[AssessedValue] [float] NOT NULL,
	[EvaluationDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.ObjectRealtyPledgers] PRIMARY KEY CLUSTERED 
(
	[ObjectRealtyPledgersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjectType]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjectType](
	[ObjectTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ObjectTypeName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_dbo.ObjectType] PRIMARY KEY CLUSTERED 
(
	[ObjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pledgers]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pledgers](
	[PledgersId] [int] IDENTITY(1,1) NOT NULL,
	[NameOfPledger] [nvarchar](max) NULL,
	[ControllingShareholder] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Pledgers] PRIMARY KEY CLUSTERED 
(
	[PledgersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Polygon]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Polygon](
	[PolygonId] [int] IDENTITY(1,1) NOT NULL,
	[PolygonName] [nvarchar](max) NULL,
	[PolygonDescription] [nvarchar](max) NULL,
	[ObjectRealtyId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Polygon] PRIMARY KEY CLUSTERED 
(
	[PolygonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Property]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Property](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[UnitId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Property] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyObjectType]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyObjectType](
	[PropertyObjectTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[ObjectTypeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PropertyObjectType] PRIMARY KEY CLUSTERED 
(
	[PropertyObjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[RegionId] [int] IDENTITY(1,1) NOT NULL,
	[RegionName] [nvarchar](100) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Region] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[Code] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Unit] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Upload]    Script Date: 14.09.2020 14:47:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Upload](
	[UploadId] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](1000) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[FileName] [nvarchar](500) NULL,
	[FileHeader] [nvarchar](max) NULL,
	[FileDescription] [nvarchar](max) NULL,
	[ObjectRealtyId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Upload] PRIMARY KEY CLUSTERED 
(
	[UploadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 14.09.2020 14:47:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 14.09.2020 14:47:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegionId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_RegionId] ON [dbo].[City]
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ObjectRealtyId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_ObjectRealtyId] ON [dbo].[Comment]
(
	[ObjectRealtyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CityId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_CityId] ON [dbo].[District]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ObjectRealtyId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_ObjectRealtyId] ON [dbo].[ObjectProperty]
(
	[ObjectRealtyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PropertyId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_PropertyId] ON [dbo].[ObjectProperty]
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CityId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_CityId] ON [dbo].[ObjectRealty]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CountryId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_CountryId] ON [dbo].[ObjectRealty]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CurrencyId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_CurrencyId] ON [dbo].[ObjectRealty]
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DistrictId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_DistrictId] ON [dbo].[ObjectRealty]
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ObjectTypeId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_ObjectTypeId] ON [dbo].[ObjectRealty]
(
	[ObjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegionId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_RegionId] ON [dbo].[ObjectRealty]
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PledgersId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_PledgersId] ON [dbo].[ObjectRealtyPledgers]
(
	[PledgersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnitId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_UnitId] ON [dbo].[Property]
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CountryId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_CountryId] ON [dbo].[Region]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentTypeId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_DocumentTypeId] ON [dbo].[Upload]
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ObjectRealtyId]    Script Date: 14.09.2020 14:47:27 ******/
CREATE NONCLUSTERED INDEX [IX_ObjectRealtyId] ON [dbo].[Upload]
(
	[ObjectRealtyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_dbo.City_dbo.Region_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([RegionId])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_dbo.City_dbo.Region_RegionId]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comment_dbo.ObjectRealty_ObjectRealtyId] FOREIGN KEY([ObjectRealtyId])
REFERENCES [dbo].[ObjectRealty] ([ObjectRealtyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_dbo.Comment_dbo.ObjectRealty_ObjectRealtyId]
GO
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_dbo.District_dbo.City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_dbo.District_dbo.City_CityId]
GO
ALTER TABLE [dbo].[ObjectProperty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectProperty_dbo.ObjectRealty_ObjectRealtyId] FOREIGN KEY([ObjectRealtyId])
REFERENCES [dbo].[ObjectRealty] ([ObjectRealtyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectProperty] CHECK CONSTRAINT [FK_dbo.ObjectProperty_dbo.ObjectRealty_ObjectRealtyId]
GO
ALTER TABLE [dbo].[ObjectProperty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectProperty_dbo.Property_PropertyId] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Property] ([PropertyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectProperty] CHECK CONSTRAINT [FK_dbo.ObjectProperty_dbo.Property_PropertyId]
GO
ALTER TABLE [dbo].[ObjectRealty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealty_dbo.City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectRealty] CHECK CONSTRAINT [FK_dbo.ObjectRealty_dbo.City_CityId]
GO
ALTER TABLE [dbo].[ObjectRealty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealty_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectRealty] CHECK CONSTRAINT [FK_dbo.ObjectRealty_dbo.Country_CountryId]
GO
ALTER TABLE [dbo].[ObjectRealty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealty_dbo.Currency_CurrencyId] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([CurrencyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectRealty] CHECK CONSTRAINT [FK_dbo.ObjectRealty_dbo.Currency_CurrencyId]
GO
ALTER TABLE [dbo].[ObjectRealty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealty_dbo.District_DistrictId] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO
ALTER TABLE [dbo].[ObjectRealty] CHECK CONSTRAINT [FK_dbo.ObjectRealty_dbo.District_DistrictId]
GO
ALTER TABLE [dbo].[ObjectRealty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealty_dbo.ObjectType_ObjectTypeId] FOREIGN KEY([ObjectTypeId])
REFERENCES [dbo].[ObjectType] ([ObjectTypeId])
GO
ALTER TABLE [dbo].[ObjectRealty] CHECK CONSTRAINT [FK_dbo.ObjectRealty_dbo.ObjectType_ObjectTypeId]
GO
ALTER TABLE [dbo].[ObjectRealty]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealty_dbo.Region_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([RegionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectRealty] CHECK CONSTRAINT [FK_dbo.ObjectRealty_dbo.Region_RegionId]
GO
ALTER TABLE [dbo].[ObjectRealtyPledgers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObjectRealtyPledgers_dbo.Pledgers_PledgersId] FOREIGN KEY([PledgersId])
REFERENCES [dbo].[Pledgers] ([PledgersId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjectRealtyPledgers] CHECK CONSTRAINT [FK_dbo.ObjectRealtyPledgers_dbo.Pledgers_PledgersId]
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Property_dbo.Unit_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_dbo.Property_dbo.Unit_UnitId]
GO
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Region_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_dbo.Region_dbo.Country_CountryId]
GO
ALTER TABLE [dbo].[Upload]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Upload_dbo.DocumentType_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Upload] CHECK CONSTRAINT [FK_dbo.Upload_dbo.DocumentType_DocumentTypeId]
GO
ALTER TABLE [dbo].[Upload]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Upload_dbo.ObjectRealty_ObjectRealtyId] FOREIGN KEY([ObjectRealtyId])
REFERENCES [dbo].[ObjectRealty] ([ObjectRealtyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Upload] CHECK CONSTRAINT [FK_dbo.Upload_dbo.ObjectRealty_ObjectRealtyId]
GO
USE [master]
GO
ALTER DATABASE [ObjectInformation] SET  READ_WRITE 
GO
