USE Expresso
GO

UPDATE dbo.Posts
SET IsDraft = 0
WHERE PublishedTimestamp IS NOT NULL
GO
