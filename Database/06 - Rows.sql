declare @userName varchar(25)
select @userName = 'seed'

insert into [dbo].[ChangeLogExclusion] values('*', 'CreationUser')
insert into [dbo].[ChangeLogExclusion] values('*', 'CreationDateTime')
insert into [dbo].[ChangeLogExclusion] values('*', 'LastUpdateUser')
insert into [dbo].[ChangeLogExclusion] values('*', 'LastUpdateDateTime')

insert into [dbo].[Country]
    values (1, 'USA', @userName, getdate(), null, null, null)

insert into [dbo].[Currency]
    values ('US Dollar', '$', @userName, getdate(), null, null, null)

insert into [dbo].[CountryCurrency]
    values (1, 1000, @userName, getdate(), null, null, null)

insert into [HumanResources].[Employee]
    values ('John', null, 'Doe', getdate(), @userName, getdate(), null, null, null)

insert [Production].[ProductCategory]
    values ('PS4 Games', @userName, getdate(), null, null, null)

insert into [Production].[Product]
    values ('The King of Fighters XIV', 1, 29.99, 'KOF XIV', 0, @userName, getdate(), null, null, null)
insert into [Production].[Product]
    values ('Street Fighter V', 1, 19.99, 'SF V', 0, @userName, getdate(), null, null, null)
insert into [Production].[Product]
    values ('Guilty Gear Xrd REV 2', 1, 29.99, 'GG', 0, @userName, getdate(), null, null, null)
insert into [Production].[Product]
    values ('Tekken 7', 1, 39.99, 'GG', 0, @userName, getdate(), null, null, null)
insert into [Production].[Product]
    values ('Marvel vs. Campcom: Infinite', 1, 19.99, 'GG', 0, @userName, getdate(), null, null, null)

insert into [Production].[Warehouse]
    values ('W0001', 'Warehouse 0001', @userName, getdate(), null, null, null)

insert into [Production].[Warehouse]
    values ('W0002', 'Warehouse 0002', @userName, getdate(), null, null, null)

insert into [Production].[ProductInventory]
    values (1, 'W0001', 150000, 150000, @userName, getdate(), null, null, null)
insert into [Production].[ProductInventory]
    values (2, 'W0002', 120000, 120000, @userName, getdate(), null, null, null)
insert into [Production].[ProductInventory]
    values (3, 'W0001', 100000, 100000, @userName, getdate(), null, null, null)
insert into [Production].[ProductInventory]
    values (4, 'W0002', 300000, 300000, @userName, getdate(), null, null, null)
insert into [Production].[ProductInventory]
    values (5, 'W0001', 400000, 400000, @userName, getdate(), null, null, null)

insert into [Sales].[Customer]
    values ('Best Buy', 'Colleen Dunn', @userName, getdate(), null, null, null)
insert into [Sales].[Customer]
    values ('Circuit City', 'Bill McCorey', @userName, getdate(), null, null, null)
insert into [Sales].[Customer]
    values ('Game Stop', 'Michael Cooper', @userName, getdate(), null, null, null)
insert into [Sales].[Customer]
    values ('Fry''s Electronics', 'John Fry', @userName, getdate(), null, null, null)

insert into [Sales].[Shipper]
    values ('DHL', 'Ricardo A. Bartra', @userName, getdate(), null, null, null)
insert into [Sales].[Shipper]
    values ('FedEx', 'Rob Carter', @userName, getdate(), null, null, null)
insert into [Sales].[Shipper]
    values ('UPS', 'Juan R. Perez', @userName, getdate(), null, null, null)

insert into [Sales].[OrderStatus]
    values (100, 'Created', @userName, getdate(), null, null, null)
insert into [Sales].[OrderStatus]
    values (200, 'Acepted', @userName, getdate(), null, null, null)
insert into [Sales].[OrderStatus]
    values (300, 'Shipped', @userName, getdate(), null, null, null)
insert into [Sales].[OrderStatus]
    values (400, 'Delivered', @userName, getdate(), null, null, null)

insert into [Sales].[PaymentMethod]
    values(newid(), 'Credit Card', 'Payment with credit card', @userName, getdate(), null, null, null)
insert into [Sales].[PaymentMethod]
    values(newid(), 'Debit Card', 'Payment with debit card', @userName, getdate(), null, null, null)
go
