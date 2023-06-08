IF OBJECT_ID('dbo.VTransactionStatus') IS NOT NULL
	DROP VIEW [dbo].[VTransactionStatus]
GO

CREATE VIEW [dbo].[VTransactionStatus]
AS
    SELECT
        [Value] AS [Id],
        [Description] AS [Name]
    FROM
        [dbo].[EnumDescription]
    WHERE
        [Type] = 'RothschildHouse.Domain.Core.Enums.TransactionStatus'
GO

IF OBJECT_ID('dbo.VCardType') IS NOT NULL
	DROP VIEW [dbo].[VCardType]
GO

CREATE VIEW [dbo].[VCardType]
AS
    SELECT
        [Value] AS [Id],
        [Description] AS [Name]
    FROM
        [dbo].[EnumDescription]
    WHERE
        [Type] = 'RothschildHouse.Domain.Core.Enums.CardType'
GO

SELECT * FROM [dbo].[VTransactionStatus]
GO

SELECT * FROM [dbo].[VCardType]
GO
