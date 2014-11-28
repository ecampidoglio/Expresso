USE [Expresso]
GO

SET IDENTITY_INSERT [dbo].[Tags] ON
GO

INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (1, N'samuel l. jackson', 1)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (2, N'quotes', 1)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (3, N'quotes', 2)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (4, N'quotes', 3)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (5, N'food', 3)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (6, N'tv shows', 3)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (7, N'musings', 4)
GO
INSERT [dbo].[Tags] ([Id], [Name], [Post_Id]) VALUES (8, N'tv shows', 4)
GO

SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
