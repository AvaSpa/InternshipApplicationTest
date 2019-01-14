-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2018 23:28:32
-- Generated from EDMX file: D:\C#\InternshipApplicationTest\InternshipApplicationTest.WebAPI\Models\DB.edmx
-- --------------------------------------------------

If(db_id(N'InternshipApplicationTest') IS NULL)
BEGIN
	CREATE DATABASE InternshipApplicationTest
END
GO

SET QUOTED_IDENTIFIER OFF;
GO
USE [InternshipApplicationTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TestQuestionTestAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestAnswers] DROP CONSTRAINT [FK_TestQuestionTestAnswer];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicantApplicantInternship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[C_ApplicantInternship] DROP CONSTRAINT [FK_ApplicantApplicantInternship];
GO
IF OBJECT_ID(N'[dbo].[FK_InternshipApplicantInternship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[C_ApplicantInternship] DROP CONSTRAINT [FK_InternshipApplicantInternship];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Applicants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Applicants];
GO
IF OBJECT_ID(N'[dbo].[TestQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestQuestions];
GO
IF OBJECT_ID(N'[dbo].[TestAnswers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestAnswers];
GO
IF OBJECT_ID(N'[dbo].[Internships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Internships];
GO
IF OBJECT_ID(N'[dbo].[C_ApplicantInternship]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C_ApplicantInternship];
GO
IF OBJECT_ID(N'[dbo].[TestConfigurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestConfigurations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Applicants'
CREATE TABLE [dbo].[Applicants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TestQuestions'
CREATE TABLE [dbo].[TestQuestions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Statement] nvarchar(max)  NOT NULL,
    [CorrectAnswerId] int  NOT NULL
);
GO

-- Creating table 'TestAnswers'
CREATE TABLE [dbo].[TestAnswers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [N_QuestionId] int  NOT NULL
);
GO

-- Creating table 'Internships'
CREATE TABLE [dbo].[Internships] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Year] int  NOT NULL
);
GO

-- Creating table 'C_ApplicantInternship'
CREATE TABLE [dbo].[C_ApplicantInternship] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Score] smallint  NULL,
    [ApplicantPassedTheTest] bit  NULL,
    [DateTestTaken] datetime  NULL,
    [N_ApplicantId] int  NOT NULL,
    [N_InternshipId] int  NOT NULL
);
GO

-- Creating table 'TestConfigurations'
CREATE TABLE [dbo].[TestConfigurations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionNumber] smallint  NOT NULL,
    [TimeLimit] time  NOT NULL,
    [MinimumScore] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Applicants'
ALTER TABLE [dbo].[Applicants]
ADD CONSTRAINT [PK_Applicants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestQuestions'
ALTER TABLE [dbo].[TestQuestions]
ADD CONSTRAINT [PK_TestQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestAnswers'
ALTER TABLE [dbo].[TestAnswers]
ADD CONSTRAINT [PK_TestAnswers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Internships'
ALTER TABLE [dbo].[Internships]
ADD CONSTRAINT [PK_Internships]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'C_ApplicantInternship'
ALTER TABLE [dbo].[C_ApplicantInternship]
ADD CONSTRAINT [PK_C_ApplicantInternship]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestConfigurations'
ALTER TABLE [dbo].[TestConfigurations]
ADD CONSTRAINT [PK_TestConfigurations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [N_QuestionId] in table 'TestAnswers'
ALTER TABLE [dbo].[TestAnswers]
ADD CONSTRAINT [FK_TestQuestionTestAnswer]
    FOREIGN KEY ([N_QuestionId])
    REFERENCES [dbo].[TestQuestions]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestQuestionTestAnswer'
CREATE INDEX [IX_FK_TestQuestionTestAnswer]
ON [dbo].[TestAnswers]
    ([N_QuestionId]);
GO

-- Creating foreign key on [N_ApplicantId] in table 'C_ApplicantInternship'
ALTER TABLE [dbo].[C_ApplicantInternship]
ADD CONSTRAINT [FK_ApplicantApplicantInternship]
    FOREIGN KEY ([N_ApplicantId])
    REFERENCES [dbo].[Applicants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicantApplicantInternship'
CREATE INDEX [IX_FK_ApplicantApplicantInternship]
ON [dbo].[C_ApplicantInternship]
    ([N_ApplicantId]);
GO

-- Creating foreign key on [N_InternshipId] in table 'C_ApplicantInternship'
ALTER TABLE [dbo].[C_ApplicantInternship]
ADD CONSTRAINT [FK_InternshipApplicantInternship]
    FOREIGN KEY ([N_InternshipId])
    REFERENCES [dbo].[Internships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InternshipApplicantInternship'
CREATE INDEX [IX_FK_InternshipApplicantInternship]
ON [dbo].[C_ApplicantInternship]
    ([N_InternshipId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------