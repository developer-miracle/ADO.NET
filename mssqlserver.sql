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
	[name] NVARCHAR(200),
);

CREATE TABLE [Authors]
(
	[id] INT IDENTITY PRIMARY KEY,
	[name] NVARCHAR(200),
);

CREATE TABLE [Books]
(
	[id] INT IDENTITY PRIMARY KEY,
	[name] NVARCHAR(200),
	[price] MONEY,
	[author_id] INT REFERENCES [Authors]([id]),
	[category_id] INT REFERENCES [Categories]([id])
);


DROP DATABASE [BOOKSTORAGE];
CREATE DATABASE [BOOKSTORAGE_TEST];
DROP DATABASE [BOOKSTORAGE_TEST];
USE [BOOKSTORAGE];


USE [BOOKSTORAGE];

INSERT INTO [dbo].[Authors] ([name]) 
VALUES
(N'Рэй Брэдбери'),
(N'Стивен Кинг'),
(N'Айзек Азимов')

INSERT INTO [dbo].[Categories] ([name]) 
VALUES
(N'фантастика'),
(N'классика'),
(N'бизнес'),
(N'роман'),
(N'фэнтези'),
(N'психология'),
(N'изотерика'),
(N'мистика'),
(N'ужасы')

INSERT INTO [dbo].[Books] ([name], [price], [author_id], [category_id]) 
VALUES 
(N'541 градус по Фаренгейту', 900.50, (SELECT [id] FROM [Authors] WHERE [name] = N'Рэй Брэдбери'), (SELECT [id] FROM [Categories] WHERE [name] = N'фантастика')),
(N'Вино из одуванчиков', 850, (SELECT [id] FROM [Authors] WHERE [name] = N'Рэй Брэдбери'), (SELECT [id] FROM [Categories] WHERE [name] = N'фантастика')),
(N'Тёмный карнавал', 700, (SELECT [id] FROM [Authors] WHERE [name] = N'Рэй Брэдбери'), (SELECT [id] FROM [Categories] WHERE [name] = N'фантастика')),
(N'Оно', 550, (SELECT [id] FROM [Authors] WHERE [name] = N'Стивен Кинг'), (SELECT [id] FROM [Categories] WHERE [name] = N'ужасы')),
(N'Зеленая миля', 670, (SELECT [id] FROM [Authors] WHERE [name] = N'Стивен Кинг'), (SELECT [id] FROM [Categories] WHERE [name] = N'мистика')),
(N'11/22/63', 495, (SELECT [id] FROM [Authors] WHERE [name] = N'Стивен Кинг'), (SELECT [id] FROM [Categories] WHERE [name] = N'фантастика')),
(N'Я, робот', 495, (SELECT [id] FROM [Authors] WHERE [name] = N'Айзек Азимов'), (SELECT [id] FROM [Categories] WHERE [name] = N'фантастика')),
(N'Основание', 495, (SELECT [id] FROM [Authors] WHERE [name] = N'Айзек Азимов'), (SELECT [id] FROM [Categories] WHERE [name] = N'роман')),
(N'Конец вечности', 495, (SELECT [id] FROM [Authors] WHERE [name] = N'Айзек Азимов'), (SELECT [id] FROM [Categories] WHERE [name] = N'фантастика'))


USE [BOOKSTORAGE];
DROP TABLE [Books]; 

DROP TABLE [Categories]; 



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











