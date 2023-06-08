IF OBJECT_ID('dbo.TransactionLog') IS NOT NULL
	DROP TABLE [dbo].[TransactionLog]
GO

IF OBJECT_ID('dbo.Transaction') IS NOT NULL
	DROP TABLE [dbo].[Transaction]
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

IF OBJECT_ID('dbo.Country') IS NOT NULL
	DROP TABLE [dbo].[Country]
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
	[Active] BIT NOT NULL
)
GO

CREATE TABLE [dbo].[Country]
(
	[Id] SMALLINT NOT NULL IDENTITY(1, 1),
	[Name] NVARCHAR(100) NOT NULL,
	[TwoLetterIsoCode] NVARCHAR(2) NOT NULL,
	[ThreeLetterIsoCode] NVARCHAR(3) NOT NULL,
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
	[AlienGuid] UNIQUEIDENTIFIER NULL,
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

CREATE TABLE [dbo].[Transaction]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[Guid] UNIQUEIDENTIFIER NOT NULL,
	[TransactionDateTime] DATETIME NULL,
	[TransactionTypeId] SMALLINT NOT NULL,
	[TransactionStatusId] SMALLINT NOT NULL,
	[ClientApplicationId] UNIQUEIDENTIFIER NOT NULL,
	[ClientFullClassName] NVARCHAR(511) NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NOT NULL,
	[StoreId] INT NOT NULL,
	[CardId] UNIQUEIDENTIFIER NOT NULL,
	[Amount] DECIMAL(12, 4) NOT NULL,
	[CurrencyId] SMALLINT NOT NULL,
	[CurrencyRate] DECIMAL(18, 4) NOT NULL,
	[Notes] NVARCHAR(MAX) NULL,
	[Active] BIT NOT NULL,
	[CreationUser] NVARCHAR(50) NOT NULL,
	[CreationDateTime] DATETIME NOT NULL,
	[LastUpdateUser] NVARCHAR(50) NULL,
	[LastUpdateDateTime] DATETIME NULL,
	[Version] ROWVERSION NULL
)
GO

CREATE TABLE [dbo].[TransactionLog]
(
	[Id] BIGINT NOT NULL IDENTITY(1, 1),
	[TransactionId] BIGINT NOT NULL,
	[TransactionStatusId] SMALLINT NOT NULL,
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
