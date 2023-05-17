USE [RothschildHouse]
GO

IF OBJECT_ID('dbo.PaymentTransactionLog') IS NOT NULL
	DROP TABLE [dbo].[PaymentTransactionLog]
GO

IF OBJECT_ID('dbo.PaymentTransaction') IS NOT NULL
	DROP TABLE [dbo].[PaymentTransaction]
GO

IF OBJECT_ID('dbo.Currency') IS NOT NULL
	DROP TABLE [dbo].[Currency]
GO

IF OBJECT_ID('dbo.ClientApplication') IS NOT NULL
	DROP TABLE [dbo].[ClientApplication]
GO

IF OBJECT_ID('dbo.Card') IS NOT NULL
	DROP TABLE [dbo].[Card]
GO

IF OBJECT_ID('dbo.Customer') IS NOT NULL
	DROP TABLE [dbo].[Customer]
GO

IF OBJECT_ID('dbo.Company') IS NOT NULL
	DROP TABLE [dbo].[Company]
GO

IF OBJECT_ID('dbo.Person') IS NOT NULL
	DROP TABLE [dbo].[Person]
GO

IF OBJECT_ID('dbo.Bank') IS NOT NULL
	DROP TABLE [dbo].[Bank]
GO

IF OBJECT_ID('dbo.EnumDescription') IS NOT NULL
	DROP TABLE [dbo].[EnumDescription]
GO

CREATE TABLE [dbo].[EnumDescription]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[Type] NVARCHAR(511) NOT NULL,
	[Value] BIGINT NOT NULL,
	[Description] NVARCHAR(200) NOT NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[Bank]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[Name] NVARCHAR(100) NOT NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[GivenName] NVARCHAR(25) NOT NULL,
	[MiddleName] NVARCHAR(25) NULL,
	[FamilyName] NVARCHAR(25) NOT NULL,
	[FullName] NVARCHAR(75) NOT NULL,
	[BirthDate] DATETIME NULL,
	[Gender] NVARCHAR(1) NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[Name] NVARCHAR(100) NOT NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[Customer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[PersonId] INT NULL,
	[CompanyId] INT NULL,
	[CountryId] SMALLINT NULL,
	[AddressLine1] NVARCHAR(100) NULL,
	[AddressLine2] NVARCHAR(100) NULL,
	[Phone] NVARCHAR(25) NULL,
	[Email] NVARCHAR(100) NULL,
	[UCommerceGuid] UNIQUEIDENTIFIER NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[Card]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[CardTypeId] SMALLINT NOT NULL,
	[IssuingNetwork] NVARCHAR(25) NOT NULL,
	[CardholderName] NVARCHAR(100) NULL,
	[CardNumber] NVARCHAR(20) NOT NULL,
	[ExpirationDate] NVARCHAR(6) NOT NULL,
	[Cvv] NVARCHAR(4) NOT NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[ClientApplication]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Url] NVARCHAR(200) NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[Currency]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[Name] NVARCHAR(50) NOT NULL,
	[Code] NVARCHAR(5) NOT NULL,
	[Rate] DECIMAL(18, 4) NOT NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[PaymentTransaction]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[Guid] UNIQUEIDENTIFIER NOT NULL,
	[ClientFullClassName] NVARCHAR(200) NOT NULL,
	[PaymentTransactionStatusId] SMALLINT NOT NULL,
	[ClientApplicationId] UNIQUEIDENTIFIER NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NOT NULL,
	[StoreId] INT NOT NULL,
	[CardId] UNIQUEIDENTIFIER NOT NULL,
	[Amount] DECIMAL(12, 4) NOT NULL,
	[CurrencyId] SMALLINT NOT NULL,
	[CurrencyRate] DECIMAL(18, 4) NOT NULL,
	[PaymentTransactionDateTime] DATETIME NULL,
	[Notes] NVARCHAR(MAX) NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[PaymentTransactionLog]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[PaymentTransactionId] BIGINT NOT NULL,
	[PaymentTransactionStatusId] SMALLINT NOT NULL,
	[LogType] NVARCHAR(25) NULL,
	[ContentType] NVARCHAR(100) NULL,
	[Content] NVARCHAR(MAX) NOT NULL,
	[Notes] NVARCHAR(MAX) NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

ALTER TABLE [dbo].[EnumDescription] ADD CONSTRAINT [PK_dbo_EnumDescription]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Bank] ADD CONSTRAINT [PK_dbo_Bank]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Person] ADD CONSTRAINT [PK_dbo_Person]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Company] ADD CONSTRAINT [PK_dbo_Company]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [PK_dbo_Customer]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [FK_dbo_Customer_PersonId_dbo_Person]
	FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person]
GO

ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [FK_dbo_Customer_CompanyId_dbo_Company]
	FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company]
GO

ALTER TABLE [dbo].[Card] ADD CONSTRAINT [PK_dbo_Card]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[ClientApplication] ADD CONSTRAINT [PK_dbo_ClientApplication]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[ClientApplication] ADD CONSTRAINT [UQ_dbo_ClientApplication_Name]
	UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Currency] ADD CONSTRAINT [PK_dbo_Currency]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[PaymentTransaction] ADD CONSTRAINT [PK_dbo_PaymentTransaction]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[PaymentTransaction] ADD CONSTRAINT [UQ_dbo_PaymentTransaction_Guid]
	UNIQUE ([Guid])
GO

ALTER TABLE [dbo].[PaymentTransaction] ADD CONSTRAINT [FK_dbo_PaymentTransaction_ClientApplicationId_dbo_ClientApplication]
	FOREIGN KEY ([ClientApplicationId]) REFERENCES [dbo].[ClientApplication]
GO

ALTER TABLE [dbo].[PaymentTransaction] ADD CONSTRAINT [FK_dbo_PaymentTransaction_CustomerId_dbo_Customer]
	FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]
GO

ALTER TABLE [dbo].[PaymentTransaction] ADD CONSTRAINT [FK_dbo_PaymentTransaction_CardId_dbo_Card]
	FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card]
GO

ALTER TABLE [dbo].[PaymentTransaction] ADD CONSTRAINT [FK_dbo_PaymentTransaction_CurrencyId_dbo_Currency]
	FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency]
GO

ALTER TABLE [dbo].[PaymentTransactionLog] ADD CONSTRAINT [PK_dbo_PaymentTransactionLog]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[PaymentTransactionLog] ADD CONSTRAINT [FK_dbo_PaymentTransactionLog_PaymentTransactionId_dbo_PaymentTransaction]
	FOREIGN KEY ([PaymentTransactionId]) REFERENCES [dbo].[PaymentTransaction]
GO
