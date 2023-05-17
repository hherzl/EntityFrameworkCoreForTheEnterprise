create function [Warehouse].[ufnGetStock](@productID int)
    returns int
as
    begin
        declare @value int
        select @value = sum([Quantity]) from [Warehouse].[ProductInventory] where [ID] = @productID
        
        if (@value is null)
            set @value = 0
        
        return @value
    end
go

create function [HumanResources].[ufnGetEmployeeFullName](@employeeID int)
    returns varchar(75)
as
    begin
        declare @value varchar(75)

        select
            @value = [FirstName] + isnull([MiddleName] + ' ', '') + [LastName]
        from
            [HumanResources].[Employee]
        where
            [ID] = @employeeID

        return @value
    end
go

create function [Sales].[ufnGetCustomerContact](@customerID int)
    returns @retCustomerContactInformation table
    (
        [CustomerID] int not null,
        [AddressLine1] varchar(50) not null,
	    [AddressLine2] varchar(50) null,
        [City] varchar(25) not null,
	    [State] varchar(25) not null,
        [ZipCode] varchar(5) null,
	    [PhoneNumber] varchar(25) null
    )
    as
        begin
            insert into
                @retCustomerContactInformation
            select
                [SalesCustomerAddress].[ID],
                [SalesCustomerAddress].[AddressLine1],
                [SalesCustomerAddress].[AddressLine2],
                [SalesCustomerAddress].[City],
                [SalesCustomerAddress].[State],
                [SalesCustomerAddress].[ZipCode],
                [SalesCustomerAddress].[PhoneNumber]
            from
                [Sales].[CustomerAddress] [SalesCustomerAddress]
            where
                [SalesCustomerAddress].[ID] = @customerID
			return
        end
go
