USE [master]
GO
/****** Object:  Database [PhysicsLab2]    Script Date: 19/11/2018 2:19:37 πμ ******/
CREATE DATABASE [PhysicsLab2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PhysicsLab2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PhysicsLab2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PhysicsLab2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PhysicsLab2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PhysicsLab2] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PhysicsLab2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PhysicsLab2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PhysicsLab2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PhysicsLab2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PhysicsLab2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PhysicsLab2] SET ARITHABORT OFF 
GO
ALTER DATABASE [PhysicsLab2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PhysicsLab2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PhysicsLab2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PhysicsLab2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PhysicsLab2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PhysicsLab2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PhysicsLab2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PhysicsLab2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PhysicsLab2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PhysicsLab2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PhysicsLab2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PhysicsLab2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PhysicsLab2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PhysicsLab2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PhysicsLab2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PhysicsLab2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PhysicsLab2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PhysicsLab2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PhysicsLab2] SET  MULTI_USER 
GO
ALTER DATABASE [PhysicsLab2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PhysicsLab2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PhysicsLab2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PhysicsLab2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PhysicsLab2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PhysicsLab2] SET QUERY_STORE = OFF
GO
USE [PhysicsLab2]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 19/11/2018 2:19:38 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[MessageData] [nvarchar](max) NOT NULL,
	[DateOfSubmission] [datetime] NULL,
	[SenderID] [int] NOT NULL,
	[ReceiverID] [int] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19/11/2018 2:19:38 πμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1, N'test', N'Hey buddy, how are you dear??', CAST(N'2018-11-16T20:01:43.527' AS DateTime), 1, 5)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (2, N'Daily program', N'Βάρα τη Βαρβάρα', CAST(N'2018-11-16T21:11:08.173' AS DateTime), 1, 15)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1002, N'Χαιρετισμός', N'Γεια σου Αρχηγέ, τι νέα;', CAST(N'2018-11-17T16:31:00.000' AS DateTime), 2, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1003, N'Απορία', N'Μήπως ξέρεις γιατί το ID πήγε στο 1002?', CAST(N'2018-11-17T23:30:58.980' AS DateTime), 1, 3)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1004, N'Goodnight', N'Go to sleep George', CAST(N'2018-11-17T23:27:55.303' AS DateTime), 1, 9)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1006, N'Καλησπέρα κύριε', N'Έχω κολλήσει εδώ, τι να κάνω?', CAST(N'2018-11-18T20:52:24.877' AS DateTime), 8, 7)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1007, N'Ευχαριστία', N'Σε ευχαριστώ που με έκανες teacher', CAST(N'2018-11-18T20:54:42.900' AS DateTime), 15, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1008, N'Goodnight', N'Goodnight Marina', CAST(N'2018-11-18T23:11:36.703' AS DateTime), 1, 3)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1009, N'Hallo', N'Hallo Vagia, how are you?', CAST(N'2018-11-18T23:40:09.680' AS DateTime), 3, 8)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1010, N'Hallo Menelae', N'This will be saved in a file as well', CAST(N'2018-11-19T00:18:51.060' AS DateTime), 1, 7)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1011, N'Grrrr', N'Γιατί δε γράφεσαι στο αρχείο?', CAST(N'2018-11-19T00:26:22.803' AS DateTime), 1, 3)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1012, N'Calm', N'Θα δοκιμάσω κι εγώ', CAST(N'2018-11-19T00:31:24.670' AS DateTime), 3, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1013, N'hallo there', N'how are you?', CAST(N'2018-11-19T00:35:14.383' AS DateTime), 5, 7)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1014, N'Hallo', N'Ouf', CAST(N'2018-11-19T00:37:36.557' AS DateTime), 8, 5)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1015, N'Hallo', N'Dear teacher1', CAST(N'2018-11-19T00:39:20.473' AS DateTime), 15, 5)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1016, N'test', N'test', CAST(N'2018-11-19T00:46:22.317' AS DateTime), 15, 15)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1017, N'nystazo', N'na kleiso to laptop?', CAST(N'2018-11-19T01:10:50.337' AS DateTime), 9, 5)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1018, N'nystazo leo', N'tha paixei?', CAST(N'2018-11-19T01:12:41.227' AS DateTime), 9, 5)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1019, N'γεια σου', N'τι καν''ς', CAST(N'2018-11-19T01:14:16.843' AS DateTime), 13, 8)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1020, N'geia', N'eisai zontanos?', CAST(N'2018-11-19T01:16:57.557' AS DateTime), 2, 9)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1021, N'geia', N'ti kaneis', CAST(N'2018-11-19T01:22:44.663' AS DateTime), 3, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1022, N'titlos', N'soma minimatos', CAST(N'2018-11-19T01:28:46.890' AS DateTime), 2, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1023, N'geia xana', N'tapaixa', CAST(N'2018-11-19T01:37:49.693' AS DateTime), 3, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1024, N'ta kataferame', N'Επιτέλους γράφει σε αρχείο!!', CAST(N'2018-11-19T01:40:58.957' AS DateTime), 7, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1025, N'Χάρηκα', N'Ναι ωραία!', CAST(N'2018-11-19T01:42:48.600' AS DateTime), 1, 7)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1026, N'Hallo', N'Lets go to sleep', CAST(N'2018-11-19T01:46:52.200' AS DateTime), 5, 1)
INSERT [dbo].[Messages] ([MessageID], [Title], [MessageData], [DateOfSubmission], [SenderID], [ReceiverID]) VALUES (1027, N'ki allo minima', N'gia na do tin triti grammi', CAST(N'2018-11-19T01:47:58.443' AS DateTime), 5, 8)
SET IDENTITY_INSERT [dbo].[Messages] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (1, N'admin     ', N'admin     ', N'Αρχοντής', N'Αρχηγόπουλος', 1, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (2, N'admin1    ', N'12345     ', N'Βασίλης', N'Βασιλόπουλος', 2, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (3, N'admin2    ', N'12345     ', N'Μαρίνα', N'Μπέλλα', 2, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (5, N'teacher1  ', N'12345     ', N'Δέσποινα', N'Δασκαλοπούλου', 3, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (7, N'teacher2  ', N'12345     ', N'Μενέλαος', N'Μέντορας', 3, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (8, N'student1  ', N'12345     ', N'Βάγια', N'Μπάκα', 4, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (9, N'student2  ', N'12345     ', N'Γιώργος', N'Κατσούλης', 4, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (10, N'fjxg      ', N'gxjgh     ', N'ghj', N'jgh', 4, 1)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (13, N'natalydimou', N'12345', N'Ναταλία', N'Δήμου', 4, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (15, N'barbara', N'12345', N'Βαρβαρούλα', N'Μπάκα', 3, 0)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [FirstName], [LastName], [Role], [Deleted]) VALUES (1015, N'babis', N'123456', N'Χαράλαμπος2', N'Βασιλόπουλος2', 3, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users]    Script Date: 19/11/2018 2:19:38 πμ ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users] FOREIGN KEY([SenderID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users1] FOREIGN KEY([ReceiverID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users1]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [CK_Messages] CHECK  ((len([MessageData])<=(250)))
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [CK_Messages]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users] CHECK  (([Role]>(0) AND [Role]<(5)))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users]
GO
USE [master]
GO
ALTER DATABASE [PhysicsLab2] SET  READ_WRITE 
GO
