USE [master]
GO
/****** Object:  Database [Quilt4]    Script Date: 2015-11-29 22:37:24 ******/
CREATE DATABASE [Quilt4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Quilt4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Quilt4.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Quilt4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Quilt4_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Quilt4] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Quilt4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Quilt4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Quilt4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Quilt4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Quilt4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Quilt4] SET ARITHABORT OFF 
GO
ALTER DATABASE [Quilt4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Quilt4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Quilt4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Quilt4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Quilt4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Quilt4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Quilt4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Quilt4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Quilt4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Quilt4] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Quilt4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Quilt4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Quilt4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Quilt4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Quilt4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Quilt4] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Quilt4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Quilt4] SET RECOVERY FULL 
GO
ALTER DATABASE [Quilt4] SET  MULTI_USER 
GO
ALTER DATABASE [Quilt4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Quilt4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Quilt4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Quilt4] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Quilt4] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Quilt4', N'ON'
GO
USE [Quilt4]
GO
/****** Object:  User [ReadUser]    Script Date: 2015-11-29 22:37:24 ******/
CREATE USER [ReadUser] FOR LOGIN [ReadUser] WITH DEFAULT_SCHEMA=[Read]
GO
/****** Object:  Schema [Read]    Script Date: 2015-11-29 22:37:24 ******/
CREATE SCHEMA [Read]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Application]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Application](
	[ApplicationKey] [uniqueidentifier] NOT NULL,
	[ProjectKey] [uniqueidentifier] NOT NULL,
	[Name] [varchar](2048) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Applicat__3214EC07F22A21D7] PRIMARY KEY CLUSTERED 
(
	[ApplicationKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2015-11-29 22:37:24 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2015-11-29 22:37:24 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2015-11-29 22:37:24 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2015-11-29 22:37:24 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2015-11-29 22:37:24 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Issue]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Issue](
	[IssueKey] [uniqueidentifier] NOT NULL,
	[IssueTypeKey] [uniqueidentifier] NOT NULL,
	[ClientCreationDate] [datetime] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[SessionKey] [uniqueidentifier] NOT NULL,
	[MachineKey] [uniqueidentifier] NOT NULL,
	[UserDataKey] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IssueKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IssueData]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IssueData](
	[Id] [uniqueidentifier] NOT NULL,
	[IssueId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IssueType]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IssueType](
	[IssueTypeKey] [uniqueidentifier] NOT NULL,
	[VersionKey] [uniqueidentifier] NOT NULL,
	[Type] [varchar](2048) NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Message] [varchar](max) NULL,
	[StackTrace] [varchar](max) NULL,
	[Ticket] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK__IssueTyp__3214EC0747267912] PRIMARY KEY CLUSTERED 
(
	[IssueTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IssueTypePageIssue]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IssueTypePageIssue](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[VersionId] [uniqueidentifier] NOT NULL,
	[IssueTypeId] [uniqueidentifier] NOT NULL,
	[Time] [datetime] NOT NULL,
	[IssueUser] [nvarchar](max) NULL,
	[Enviroment] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IssueTypePageIssueType]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IssueTypePageIssueType](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[VersionId] [uniqueidentifier] NOT NULL,
	[ProjectName] [nvarchar](50) NOT NULL,
	[ApplicationName] [nvarchar](max) NOT NULL,
	[Version] [nvarchar](max) NOT NULL,
	[Ticket] [int] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Machine]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[MachineKey] [uniqueidentifier] NOT NULL,
	[Fingerprint] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MachineKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MachineData]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MachineData](
	[Id] [uniqueidentifier] NOT NULL,
	[MachineId] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectKey] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DashboardColor] [nvarchar](20) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProjectApiKey] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [NO_DUP_TOKEN] UNIQUE NONCLUSTERED 
(
	[ProjectApiKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectPageApplication]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPageApplication](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Versions] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectPageProject]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPageProject](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DashboardColor] [nvarchar](20) NOT NULL DEFAULT ('blue'),
	[ProjectApiKey] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectPageVersion]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPageVersion](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Version] [nvarchar](max) NOT NULL,
	[Sessions] [int] NOT NULL,
	[IssueTypes] [int] NOT NULL,
	[Issues] [int] NOT NULL,
	[Last] [datetime] NULL,
	[Enviroments] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionKey] [uniqueidentifier] NOT NULL,
	[ClientStartTime] [datetime] NOT NULL,
	[ServerStartTime] [datetime] NOT NULL,
	[ClientEndTime] [datetime] NULL,
	[ServerEndTime] [datetime] NULL,
	[CallerIp] [nvarchar](45) NOT NULL,
	[ApplicationKey] [uniqueidentifier] NOT NULL,
	[VersionKey] [uniqueidentifier] NOT NULL,
	[UserDataKey] [uniqueidentifier] NOT NULL,
	[MachineKey] [uniqueidentifier] NOT NULL,
	[Enviroment] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SessionKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserData]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserData](
	[UserDataKey] [uniqueidentifier] NOT NULL,
	[Fingerprint] [varchar](128) NOT NULL,
	[UserName] [varchar](256) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK__UserData__3214EC0718AE78FB] PRIMARY KEY CLUSTERED 
(
	[UserDataKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Version]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Version](
	[VersionKey] [uniqueidentifier] NOT NULL,
	[ApplicationKey] [uniqueidentifier] NOT NULL,
	[Version] [varchar](512) NOT NULL,
	[SupportToolkitVersion] [varchar](512) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Version__3214EC073BE89274] PRIMARY KEY CLUSTERED 
(
	[VersionKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VersionPageIssueType]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionPageIssueType](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[VersionId] [uniqueidentifier] NOT NULL,
	[Ticket] [int] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Issues] [int] NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[LastIssue] [datetime] NULL,
	[Enviroments] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VersionPageVersion]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionPageVersion](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[ApplicaitonId] [uniqueidentifier] NOT NULL,
	[ProjectName] [nvarchar](50) NOT NULL,
	[ApplicationName] [nvarchar](max) NOT NULL,
	[Version] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Read].[DashboardPageProject]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Read].[DashboardPageProject](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectKey] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[VersionCount] [int] NOT NULL CONSTRAINT [DF__Dashboard__Versi__2F9A1060]  DEFAULT ((0)),
	[SessionCount] [int] NOT NULL CONSTRAINT [DF__Dashboard__Sessi__308E3499]  DEFAULT ((0)),
	[IssueTypeCount] [int] NOT NULL CONSTRAINT [DF__Dashboard__Issue__318258D2]  DEFAULT ((0)),
	[IssueCount] [int] NOT NULL CONSTRAINT [DF__Dashboard__Issue__32767D0B]  DEFAULT ((0)),
	[DashboardColor] [varchar](20) NOT NULL,
 CONSTRAINT [PK__Dashboar__761ABEF08DFD73F1] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Read].[ProjectPageProject]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Read].[ProjectPageProject](
	[ProjectKey] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DashboardColor] [nvarchar](20) NOT NULL,
	[ProjectApiKey] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__ProjectP__C048AC9439D7FF40] PRIMARY KEY CLUSTERED 
(
	[ProjectKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Read].[User]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Read].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK__User__1788CC4CCACECB05] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Read].[UserProject]    Script Date: 2015-11-29 22:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Read].[UserProject](
	[UserId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 2015-11-29 22:37:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2015-11-29 22:37:24 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2015-11-29 22:37:24 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 2015-11-29 22:37:24 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2015-11-29 22:37:24 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 2015-11-29 22:37:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ProjectApiKey]    Script Date: 2015-11-29 22:37:24 ******/
CREATE UNIQUE NONCLUSTERED INDEX [ProjectApiKey] ON [dbo].[Project]
(
	[ProjectApiKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectPageApplication] ADD  DEFAULT ((0)) FOR [Versions]
GO
ALTER TABLE [dbo].[ProjectPageVersion] ADD  DEFAULT ((0)) FOR [Sessions]
GO
ALTER TABLE [dbo].[ProjectPageVersion] ADD  DEFAULT ((0)) FOR [IssueTypes]
GO
ALTER TABLE [dbo].[ProjectPageVersion] ADD  DEFAULT ((0)) FOR [Issues]
GO
ALTER TABLE [dbo].[VersionPageIssueType] ADD  DEFAULT ((0)) FOR [Ticket]
GO
ALTER TABLE [dbo].[VersionPageIssueType] ADD  DEFAULT ((0)) FOR [Issues]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_Project] FOREIGN KEY([ProjectKey])
REFERENCES [dbo].[Project] ([ProjectKey])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_Project]
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
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD FOREIGN KEY([MachineKey])
REFERENCES [dbo].[Machine] ([MachineKey])
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_IssueType] FOREIGN KEY([IssueTypeKey])
REFERENCES [dbo].[IssueType] ([IssueTypeKey])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_IssueType]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Machine] FOREIGN KEY([MachineKey])
REFERENCES [dbo].[Machine] ([MachineKey])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_Machine]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Session] FOREIGN KEY([SessionKey])
REFERENCES [dbo].[Session] ([SessionKey])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_Session]
GO
ALTER TABLE [dbo].[Issue]  WITH CHECK ADD  CONSTRAINT [FK_Issue_UserData] FOREIGN KEY([UserDataKey])
REFERENCES [dbo].[UserData] ([UserDataKey])
GO
ALTER TABLE [dbo].[Issue] CHECK CONSTRAINT [FK_Issue_UserData]
GO
ALTER TABLE [dbo].[IssueData]  WITH CHECK ADD FOREIGN KEY([IssueId])
REFERENCES [dbo].[Issue] ([IssueKey])
GO
ALTER TABLE [dbo].[IssueType]  WITH CHECK ADD  CONSTRAINT [FK_IssueType_Version] FOREIGN KEY([VersionKey])
REFERENCES [dbo].[Version] ([VersionKey])
GO
ALTER TABLE [dbo].[IssueType] CHECK CONSTRAINT [FK_IssueType_Version]
GO
ALTER TABLE [dbo].[MachineData]  WITH CHECK ADD FOREIGN KEY([MachineId])
REFERENCES [dbo].[Machine] ([MachineKey])
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Application] FOREIGN KEY([ApplicationKey])
REFERENCES [dbo].[Application] ([ApplicationKey])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Application]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Machine] FOREIGN KEY([MachineKey])
REFERENCES [dbo].[Machine] ([MachineKey])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Machine]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_UserData] FOREIGN KEY([UserDataKey])
REFERENCES [dbo].[UserData] ([UserDataKey])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_UserData]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Version] FOREIGN KEY([VersionKey])
REFERENCES [dbo].[Version] ([VersionKey])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Version]
GO
ALTER TABLE [dbo].[Version]  WITH CHECK ADD  CONSTRAINT [FK_Version_Application] FOREIGN KEY([ApplicationKey])
REFERENCES [dbo].[Application] ([ApplicationKey])
GO
ALTER TABLE [dbo].[Version] CHECK CONSTRAINT [FK_Version_Application]
GO
ALTER TABLE [Read].[ProjectPageProject]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPageProject_DashboardPageProject] FOREIGN KEY([ProjectId])
REFERENCES [Read].[DashboardPageProject] ([ProjectId])
GO
ALTER TABLE [Read].[ProjectPageProject] CHECK CONSTRAINT [FK_ProjectPageProject_DashboardPageProject]
GO
ALTER TABLE [Read].[UserProject]  WITH CHECK ADD  CONSTRAINT [FK_User] FOREIGN KEY([UserId])
REFERENCES [Read].[User] ([UserId])
GO
ALTER TABLE [Read].[UserProject] CHECK CONSTRAINT [FK_User]
GO
ALTER TABLE [Read].[UserProject]  WITH CHECK ADD  CONSTRAINT [FK_UserProject] FOREIGN KEY([ProjectId])
REFERENCES [Read].[DashboardPageProject] ([ProjectId])
GO
ALTER TABLE [Read].[UserProject] CHECK CONSTRAINT [FK_UserProject]
GO
USE [master]
GO
ALTER DATABASE [Quilt4] SET  READ_WRITE 
GO
