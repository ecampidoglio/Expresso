USE Expresso
GO

CREATE TABLE dbo.TagPosts (
    Post_Id int NOT NULL,
    Tag_Id int NOT NULL,
    PRIMARY KEY(Post_Id, Tag_Id)
)
GO

ALTER TABLE dbo.TagPosts WITH CHECK
ADD CONSTRAINT FK_TagPosts_Posts FOREIGN KEY (Post_Id) REFERENCES dbo.Posts (Id)
GO

ALTER TABLE dbo.TagPosts WITH CHECK
ADD CONSTRAINT FK_TagPosts_Tags FOREIGN KEY (Tag_Id) REFERENCES dbo.Tags (Id)
GO
