CREATE TABLE[dbo].[GroceryItems](

    [Id][nvarchar](450) NOT NULL,
    [Name] [nvarchar](max)NOT NULL,
	[CreatedDate] DATETIME,
    
 CONSTRAINT[PK_Item] PRIMARY KEY CLUSTERED ([Id] ASC))