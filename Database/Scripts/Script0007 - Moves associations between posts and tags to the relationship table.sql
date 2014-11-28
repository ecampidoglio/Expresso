USE Expresso
GO

INSERT INTO dbo.TagPosts (Post_Id, Tag_Id)
SELECT Post_Id, Id
FROM dbo.Tags
GO

WITH UniqueTags (Id, Name) AS
(
	SELECT MIN(Id) AS Id, Name
	FROM dbo.Tags
	GROUP BY Name
)
UPDATE dbo.TagPosts
SET Tag_Id = ut.Id
FROM dbo.TagPosts tp
INNER JOIN dbo.Tags t
ON tp.Tag_Id = t.Id
INNER JOIN UniqueTags ut
ON t.Name = ut.Name
GO
