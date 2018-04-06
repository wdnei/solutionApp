
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/05/2018 12:49:01
-- Generated from EDMX file: C:\Users\wdneipaixao\source\repos\solutionApp\solutionApp\Models\SolutionEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [solutionApp];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Veiculo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Veiculo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Veiculo'
CREATE TABLE [dbo].[Veiculo] (
    [id] int IDENTITY(1,1) NOT NULL,
    [cor] nvarchar(max)  NOT NULL,
    [modelo] nvarchar(max)  NOT NULL,
    [placa] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Multa'
CREATE TABLE [dbo].[Multa] (
    [id] int IDENTITY(1,1) NOT NULL,
    [pontos] int  NOT NULL,
    [descricao] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MultaVeiculo'
CREATE TABLE [dbo].[MultaVeiculo] (
    [Multa_id] int  NOT NULL,
    [MultaVeiculo_Multa_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Veiculo'
ALTER TABLE [dbo].[Veiculo]
ADD CONSTRAINT [PK_Veiculo]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Multa'
ALTER TABLE [dbo].[Multa]
ADD CONSTRAINT [PK_Multa]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Multa_id], [MultaVeiculo_Multa_id] in table 'MultaVeiculo'
ALTER TABLE [dbo].[MultaVeiculo]
ADD CONSTRAINT [PK_MultaVeiculo]
    PRIMARY KEY CLUSTERED ([Multa_id], [MultaVeiculo_Multa_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Multa_id] in table 'MultaVeiculo'
ALTER TABLE [dbo].[MultaVeiculo]
ADD CONSTRAINT [FK_MultaVeiculo_Multa]
    FOREIGN KEY ([Multa_id])
    REFERENCES [dbo].[Multa]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MultaVeiculo_Multa_id] in table 'MultaVeiculo'
ALTER TABLE [dbo].[MultaVeiculo]
ADD CONSTRAINT [FK_MultaVeiculo_Veiculo]
    FOREIGN KEY ([MultaVeiculo_Multa_id])
    REFERENCES [dbo].[Veiculo]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MultaVeiculo_Veiculo'
CREATE INDEX [IX_FK_MultaVeiculo_Veiculo]
ON [dbo].[MultaVeiculo]
    ([MultaVeiculo_Multa_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------