IF OBJECT_ID('dbo.VPaymentTransactionStatus') IS NOT NULL
	DROP VIEW [dbo].[VPaymentTransactionStatus]
GO

CREATE VIEW [dbo].[VPaymentTransactionStatus]
AS
    SELECT
        [Value] AS [Id],
        [Description] AS [Name]
    FROM
        [dbo].[EnumDescription]
    WHERE
        [Type] = 'RothschildHouse.API.PaymentGateway.Domain.Enums.PaymentTransactionStatus'
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
        [Type] = 'RothschildHouse.API.PaymentGateway.Domain.Enums.CardType'
GO

SELECT * FROM [dbo].[VPaymentTransactionStatus]
GO

SELECT * FROM [dbo].[VCardType]
GO
