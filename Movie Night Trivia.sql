USE [master]
GO

CREATE DATABASE [MovieNightTrivia]
 
CREATE TABLE [dbo].[Game](
	[gameId] [varchar](5) NOT NULL,
	[playerName] [varchar](50) NOT NULL,
	[playerId] [uniqueidentifier] NOT NULL,
	[opponentId] [uniqueidentifier] NULL,
	[opponentName] [varchar](50) NULL,
	[isHostCompleted] [bit] NOT NULL,
	[isOpponentCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[gameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerAnswer]    Script Date: 2020/10/12 16:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerAnswer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[playerId] [uniqueidentifier] NOT NULL,
	[gameId] [varchar](50) NOT NULL,
	[movieRank] [int] NOT NULL,
	[year] [int] NOT NULL,
 CONSTRAINT [PK_PlayerAnswer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_isHostCompleted]  DEFAULT ((0)) FOR [isHostCompleted]
GO
ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_isOpponentCompleted]  DEFAULT ((0)) FOR [isOpponentCompleted]
GO
USE [master]
GO
ALTER DATABASE [MovieNightTrivia] SET  READ_WRITE 
GO
