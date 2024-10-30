-- -------------------------------------------------------------
-- TablePlus 6.1.2(568)
--
-- https://tableplus.com/
--
-- Database: SimpleShop
-- Generation Time: 2567-10-30 21:03:21.4790
-- -------------------------------------------------------------


-- This script only contains the table creation statements and does not fully represent the table in the database. It's still missing: sequences, indices, triggers. Do not use it as a backup.
CREATE DATABASE SimpleShop;
GO

USE SimpleShop;
GO CREATE TABLE [dbo].[Customer] (
	[Id] uniqueidentifier,
	[Name] varchar(255),
	[Email] varchar(100),
	[CreatedAt] datetime2 (7),
	[UpdatedAt] datetime2 (7),
	PRIMARY KEY ([Id])
);
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Email], [CreatedAt], [UpdatedAt]) VALUES
('A5265F32-EF0C-49E5-98E3-08DCF752F069', N'sakulrat', N'sakulrat.ra@gmail.com', '0001-01-01 00:00:00.0000000', '2024-10-28 21:06:00.5562360');
GO