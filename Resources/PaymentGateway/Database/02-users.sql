IF NOT EXISTS(SELECT name FROM sys.sysusers WHERE name = 'rothschild-house.api.payment-gateway')
    BEGIN
        CREATE USER [rothschild-house.api.payment-gateway] FOR LOGIN [rothschild-house.api.payment-gateway]
        EXEC [sp_addrolemember] N'db_owner', N'rothschild-house.api.payment-gateway'
    END