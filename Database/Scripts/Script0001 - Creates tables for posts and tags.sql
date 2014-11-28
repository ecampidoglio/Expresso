USE [Expresso]
GO

CREATE TABLE [dbo].[Posts](
    [Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Title] nvarchar(255) NOT NULL,
    [MarkdownContent] nvarchar(max) NULL,
    [ModifiedTimestamp] datetime NOT NULL CONSTRAINT [DF_Posts_ModifiedTimestamp]  DEFAULT (getdate()),
    [PublishedTimestamp] datetime NULL
)
GO

USE [Expresso]
GO

CREATE TABLE [dbo].[Tags](
    [Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] varchar(255) NOT NULL,
    [Post_Id] int NULL
)
GO

ALTER TABLE [dbo].[Tags] ADD CONSTRAINT [FK_Tags_Posts] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Tags] CHECK CONSTRAINT [FK_Tags_Posts]
GO
