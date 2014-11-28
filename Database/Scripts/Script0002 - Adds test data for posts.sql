USE [Expresso]
GO

SET IDENTITY_INSERT [dbo].[Posts] ON
GO

INSERT [dbo].[Posts] ([Id], [Title], [MarkdownContent], [ModifiedTimestamp], [PublishedTimestamp]) VALUES (1, N'So, you cold?', N'Now that we know who you are, I know who I am. I''m not a mistake! It all makes sense! In a comic, you know how you can tell who the arch-villain''s going to be? He''s the exact opposite of the hero. And most times they''re friends, like you and me! I should''ve known way back when... You know why, David? Because of the kids. They called me Mr Glass.
', CAST(N'2014-11-23 22:19:54.093' AS DateTime), CAST(N'2014-10-04 13:37:00.000' AS DateTime))
GO
INSERT [dbo].[Posts] ([Id], [Title], [MarkdownContent], [ModifiedTimestamp], [PublishedTimestamp]) VALUES (2, N'I can do that', N'You see? It''s curious. Ted did figure it out - time travel. And when we get back, we gonna tell everyone. How it''s possible, how it''s done, what the dangers are. But then why fifty years in the future when the spacecraft encounters a black hole does the computer call it an ''unknown entry event''? Why don''t they know? If they don''t know, that means we never told anyone. And if we never told anyone it means we never made it back. Hence we die down here. Just as a matter of deductive logic.
', CAST(N'2014-11-23 22:20:50.643' AS DateTime), NULL)
GO
INSERT [dbo].[Posts] ([Id], [Title], [MarkdownContent], [ModifiedTimestamp], [PublishedTimestamp]) VALUES (3, N'Uuummmm, this is a tasty burger!', N'Your bones don''t break, mine do. That''s clear. Your cells react to bacteria and viruses differently than mine. You don''t get sick, I do. That''s also clear. But for some reason, you and I react the exact same way to water. We swallow it too fast, we choke. We get some in our lungs, we drown. However unreal it may seem, we are connected, you and I. We''re on the same curve, just on opposite ends.', CAST(N'2014-11-23 22:21:06.523' AS DateTime), CAST(N'2014-11-26 21:55:06.523' AS DateTime))
GO
INSERT [dbo].[Posts] ([Id], [Title], [MarkdownContent], [ModifiedTimestamp], [PublishedTimestamp]) VALUES (4, N'I''m serious', N'Well, the way they make shows is, they make one show. That show''s called a pilot. Then they show that show to the people who make shows, and on the strength of that one show they decide if they''re going to make more shows. Some pilots get picked and become television programs. Some don''t, become nothing. She starred in one of the ones that became nothing.
', CAST(N'2014-11-23 22:22:08.067' AS DateTime), NULL)
GO

SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
