
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/30/2013 14:37:06
-- Generated from EDMX file: C:\Programming\test BD\testEF\testEF\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [tempdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Articls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articls];
GO
IF OBJECT_ID(N'[dbo].[Chanals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chanals];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articls'
CREATE TABLE [dbo].[Articls] (
    [IdArticl] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(max)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [Source] varchar(max)  NOT NULL,
    [PubDate] datetime2  NOT NULL,
    [Readed] bit  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
    [IdChanal] int NOT NULL
);
GO

-- Creating table 'Chanals'
CREATE TABLE [dbo].[Chanals] (
    [IdChanal] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(max)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [Source] varchar(max)  NOT NULL,
    [PubDate] datetime2  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdArticl] in table 'Articls'
ALTER TABLE [dbo].[Articls]
ADD CONSTRAINT [PK_Articls]
    PRIMARY KEY CLUSTERED ([IdArticl] ASC);
GO

-- Creating primary key on [IdChanal] in table 'Chanals'
ALTER TABLE [dbo].[Chanals]
ADD CONSTRAINT [PK_Chanals]
    PRIMARY KEY CLUSTERED ([IdChanal] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------