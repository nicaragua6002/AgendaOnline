
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/17/2023 12:28:25
-- Generated from EDMX file: C:\Users\solis\source\repos\AgendaOnline\AgendaOnline\Models\AgendaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AgendaOnlineBD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellidos] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Telf] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Contactos'
CREATE TABLE [dbo].[Contactos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellido] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Telf] nvarchar(max)  NOT NULL,
    [GrupoId] int  NOT NULL
);
GO

-- Creating table 'Grupos'
CREATE TABLE [dbo].[Grupos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Color] nvarchar(max)  NOT NULL,
    [UsuarioId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contactos'
ALTER TABLE [dbo].[Contactos]
ADD CONSTRAINT [PK_Contactos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Grupos'
ALTER TABLE [dbo].[Grupos]
ADD CONSTRAINT [PK_Grupos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsuarioId] in table 'Grupos'
ALTER TABLE [dbo].[Grupos]
ADD CONSTRAINT [FK_UsuarioGrupo]
    FOREIGN KEY ([UsuarioId])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioGrupo'
CREATE INDEX [IX_FK_UsuarioGrupo]
ON [dbo].[Grupos]
    ([UsuarioId]);
GO

-- Creating foreign key on [GrupoId] in table 'Contactos'
ALTER TABLE [dbo].[Contactos]
ADD CONSTRAINT [FK_GrupoContacto]
    FOREIGN KEY ([GrupoId])
    REFERENCES [dbo].[Grupos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GrupoContacto'
CREATE INDEX [IX_FK_GrupoContacto]
ON [dbo].[Contactos]
    ([GrupoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------