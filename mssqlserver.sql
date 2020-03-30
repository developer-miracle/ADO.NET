USE ROCKETBOT;
SELECT TOP (10) [id],[Name],[Mail],[Level]
FROM [ROCKETBOT].[dbo].[Users]

USE ROCKETBOT;
INSERT INTO [dbo].[Users] ([Name], [Mail], [Level]) 
VALUES ('Miracle', 'r-9@gmail.com', 9)

USE [ROCKETBOT];
DROP TABLE Books, Categories;


CREATE DATABASE [BOOKSTORAGE];

USE [BOOKSTORAGE];

CREATE TABLE [Categories]
(
	[id] INT IDENTITY PRIMARY KEY,
	[name] VARCHAR(25),
);

USE [BOOKSTORAGE];
CREATE TABLE [Books]
(
	[id] INT IDENTITY PRIMARY KEY,
	[name] VARCHAR(25),
	[pages] INT,
	[year_press] INT,
	[category_id] INT REFERENCES [Categories]([id])
);

USE [BOOKSTORAGE];
INSERT INTO [dbo].[Categories] ([name]) 
VALUES 
('mystic'),
('postApocalypse');

USE [BOOKSTORAGE];
INSERT INTO [dbo].[Books] ([name], [pages], [year_press], [category_id]) 
VALUES 
('book1', 200, 2020, 1),
('book2', 250, 2019, 1),
('book3', 150, 2015, 2);

USE [BOOKSTORAGE];
CREATE PROCEDURE [dbo].[sp_getXML]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		[id] "@id",
		[name] "name", 
		(
			SELECT 
				[id] "@id",
				[name] "name",
				[pages] "pages",
				[year_press] "year_press"
			FROM Books AS B
			WHERE B.[category_id] = C.[id] 
			FOR XML PATH('Book'), TYPE 
		) AS [Book]
	FROM [Categories] AS C
	FOR XML PATH('Category'), ROOT('Caregories')
END

USE [BOOKSTORAGE];
CREATE PROCEDURE [dbo].[sp_getJSON]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		[id] "@id",
		[name] "name", 
		(
			SELECT 
				[id] "@id",
				[name] "name",
				[pages] "pages",
				[year_press] "year_press"
			FROM [Books] AS B
			WHERE B.[category_id] = C.[id] 
			FOR JSON PATH
			) AS [Book]
	FROM [Categories] AS C
	FOR JSON PATH, ROOT('Caregories')
END

EXEC [dbo].[sp_getXML];

EXEC [dbo].[sp_getJSON];











