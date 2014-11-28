USE Expresso
GO

DELETE
FROM dbo.Tags
WHERE Id NOT IN (
    SELECT DISTINCT Tag_Id
    FROM dbo.TagPosts
)
GO
