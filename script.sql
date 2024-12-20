Build started...
Build succeeded.
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Comments] (
    [CommentId] int NOT NULL IDENTITY,
    [Content] nvarchar(max) NOT NULL,
    [ParentId] int NOT NULL,
    [UserId] int NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([CommentId])
);
GO

CREATE TABLE [Recipes] (
    [RecipeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Steps] nvarchar(max) NOT NULL,
    [Stars] real NOT NULL,
    [FavCount] int NOT NULL,
    [ImagePath] nvarchar(max) NOT NULL,
    [UserId] int NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Recipes] PRIMARY KEY ([RecipeId])
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [RecipeReads] (
    [RecipeReadId] int NOT NULL IDENTITY,
    [RecipeId] int NOT NULL,
    [UserId] int NOT NULL,
    [StarsRating] int NOT NULL,
    [Favorite] bit NOT NULL,
    CONSTRAINT [PK_RecipeReads] PRIMARY KEY ([RecipeReadId]),
    CONSTRAINT [FK_RecipeReads_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([RecipeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_RecipeReads_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RecipeReads_RecipeId] ON [RecipeReads] ([RecipeId]);
GO

CREATE INDEX [IX_RecipeReads_UserId] ON [RecipeReads] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241109200306_InitialCreate', N'8.0.10');
GO

COMMIT;
GO


