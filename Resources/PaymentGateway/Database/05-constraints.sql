ALTER TABLE [dbo].[EnumDescription] ADD CONSTRAINT [PK_dbo_EnumDescription]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Country] ADD CONSTRAINT [PK_dbo_Country]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Country] ADD CONSTRAINT [UQ_dbo_Country_Name]
	UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Country] ADD CONSTRAINT [UQ_dbo_Country_TwoLetterIsoCode]
	UNIQUE ([TwoLetterIsoCode])
GO

ALTER TABLE [dbo].[Country] ADD CONSTRAINT [UQ_dbo_Country_ThreeLetterIsoCode]
	UNIQUE ([ThreeLetterIsoCode])
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

ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [FK_dbo_Customer_CountryId_dbo_Country]
	FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country]
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

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [PK_dbo_Transaction]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [UQ_dbo_Transaction_Guid]
	UNIQUE ([Guid])
GO

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_dbo_Transaction_ClientApplicationId_dbo_ClientApplication]
	FOREIGN KEY ([ClientApplicationId]) REFERENCES [dbo].[ClientApplication]
GO

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_dbo_Transaction_CustomerId_dbo_Customer]
	FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]
GO

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_dbo_Transaction_CardId_dbo_Card]
	FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card]
GO

ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT [FK_dbo_Transaction_CurrencyId_dbo_Currency]
	FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency]
GO

ALTER TABLE [dbo].[TransactionLog] ADD CONSTRAINT [PK_dbo_TransactionLog]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[TransactionLog] ADD CONSTRAINT [FK_dbo_TransactionLog_TransactionId_dbo_Transaction]
	FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[Transaction]
GO
