USE [bitly]
GO

/****** Object:  Table [dbo].[Urls]    Script Date: 04/06/2016 22:53:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Urls](
	[Id] [uniqueidentifier] NOT NULL,
	[OriginUrl] [varchar](900) NOT NULL,
	[ShortUrl] [varchar](900) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Traffic] [bigint] NOT NULL,
 CONSTRAINT [PK_Urls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE UNIQUE INDEX idx_urls_originurl
   ON Urls (OriginUrl);   
GO

CREATE UNIQUE INDEX idx_urls_shorturl
   ON Urls (ShortUrl);   
GO


