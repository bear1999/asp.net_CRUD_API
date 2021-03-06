USE [master]
GO
/****** Object:  Database [EmployeeDB]    Script Date: 5/7/2021 1:46:56 AM ******/
CREATE DATABASE [EmployeeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmployeeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\EmployeeDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EmployeeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\EmployeeDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EmployeeDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EmployeeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EmployeeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EmployeeDB] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeeDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EmployeeDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EmployeeDB] SET QUERY_STORE = OFF
GO
USE [EmployeeDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [EmployeeDB]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/7/2021 1:46:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[ProfileImage] [nvarchar](100) NULL,
	[idRole] [int] NOT NULL,
	[Password] [varchar](60) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees_Info]    Script Date: 5/7/2021 1:46:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees_Info](
	[idEmployee] [uniqueidentifier] NOT NULL,
	[Fullname] [nvarchar](50) NULL,
	[Birthday] [date] NULL,
	[PhoneNumber] [varchar](16) NULL,
	[Address] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employees_Info] PRIMARY KEY CLUSTERED 
(
	[idEmployee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/7/2021 1:46:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[idRole] [int] IDENTITY(1,1) NOT NULL,
	[name_role] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Employees] ([Id], [Username], [ProfileImage], [idRole], [Password]) VALUES (N'14ce9a1f-f0dc-4849-b966-4222732f4179', N'bearon1', N'07052021_5d6ea075-86b8-46f8-b19f-2ad1cbe04a34.png', 1, N'$2a$11$BI/AruNFVzsI.0oiqJ1SwejS6T9OQULX5nZIg444IZAOKoAiGfxam')
INSERT [dbo].[Employees] ([Id], [Username], [ProfileImage], [idRole], [Password]) VALUES (N'57b4a8d9-a1fd-4f1e-9660-b84c6fc78455', N'bearof3', N'03052021_c31faafb-0020-487d-8a76-0d91266a913c.png', 1, N'$2a$11$lf4GP.jk8MHyIUTrLmHm2ewaQtEVNGhZV.JdmzXznYC1yS5Hwzdr6')
INSERT [dbo].[Employees] ([Id], [Username], [ProfileImage], [idRole], [Password]) VALUES (N'50fe3996-2c69-4305-a9f8-c8754b2318d9', N'bearof2', N'03052021_129efb39-3e90-4927-b595-a7d78e600cb4.png', 1, N'$2a$11$2C0ekYoGwRbKmoj6Vd0y6OhVyjOhJSvXK8FapKH5q7wVw94Tjnxhu')
INSERT [dbo].[Employees] ([Id], [Username], [ProfileImage], [idRole], [Password]) VALUES (N'3e8b749b-a1d5-44cc-a990-cb9c018edb7d', N'bearon', N'03052021_7d9ef824-6545-472f-8773-24b2839088d1.png', 1, N'$2a$11$G3X8Zor5kEqJ65aEyRS6Qumzm4iz8M.3fiMk/.rQ5N/mnrVKBdkCC')
GO
INSERT [dbo].[Employees_Info] ([idEmployee], [Fullname], [Birthday], [PhoneNumber], [Address]) VALUES (N'14ce9a1f-f0dc-4849-b966-4222732f4179', N'Đỗ Ngọc Sỹ', CAST(N'1999-05-05' AS Date), N'0586209147', N'Go Vap')
INSERT [dbo].[Employees_Info] ([idEmployee], [Fullname], [Birthday], [PhoneNumber], [Address]) VALUES (N'57b4a8d9-a1fd-4f1e-9660-b84c6fc78455', N'Đỗ Ngọc Sỹ', CAST(N'1999-05-05' AS Date), N'0586209147', N'Go Vap')
INSERT [dbo].[Employees_Info] ([idEmployee], [Fullname], [Birthday], [PhoneNumber], [Address]) VALUES (N'50fe3996-2c69-4305-a9f8-c8754b2318d9', N'Đỗ Ngọc Sỹ', CAST(N'1999-05-05' AS Date), N'0586209147', N'Go Vap')
INSERT [dbo].[Employees_Info] ([idEmployee], [Fullname], [Birthday], [PhoneNumber], [Address]) VALUES (N'3e8b749b-a1d5-44cc-a990-cb9c018edb7d', N'Đỗ Ngọc Sỹ', CAST(N'1999-05-05' AS Date), N'0586209147', N'Go Vap')
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([idRole], [name_role]) VALUES (1, N'User')
INSERT [dbo].[Roles] ([idRole], [name_role]) VALUES (2, N'Employee')
INSERT [dbo].[Roles] ([idRole], [name_role]) VALUES (3, N'Administrator')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UNIQUE_Username]    Script Date: 5/7/2021 1:46:56 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UNIQUE_Username] ON [dbo].[Employees]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Roles] FOREIGN KEY([idRole])
REFERENCES [dbo].[Roles] ([idRole])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Roles]
GO
ALTER TABLE [dbo].[Employees_Info]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Info_Employees] FOREIGN KEY([idEmployee])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Employees_Info] CHECK CONSTRAINT [FK_Employees_Info_Employees]
GO
USE [master]
GO
ALTER DATABASE [EmployeeDB] SET  READ_WRITE 
GO
