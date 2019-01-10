declare @userName varchar(25), @creationDateTime datetime
select @userName = 'seed', @creationDateTime = getdate()

insert into [dbo].[ChangeLogExclusion] values('*', 'CreationUser')
insert into [dbo].[ChangeLogExclusion] values('*', 'CreationDateTime')
insert into [dbo].[ChangeLogExclusion] values('*', 'LastUpdateUser')
insert into [dbo].[ChangeLogExclusion] values('*', 'LastUpdateDateTime')

insert into [dbo].[Country]
    values (1, 'USA', @userName, @creationDateTime, null, null, null)

insert into [dbo].[Currency]
    values ('USD', 'US Dollar', '$', @userName, @creationDateTime, null, null, null)

insert into [dbo].[CountryCurrency]
    values ('1', 'USD', @userName, @creationDateTime, null, null, null)

insert into [HumanResources].[Employee]
    values ('John', null, 'Doe', getdate(), @userName, @creationDateTime, null, null, null)

insert [Warehouse].[ProductCategory]
    values ('PS4 Games', @userName, getdate(), null, null, null)

insert into [Warehouse].[Product]
    values ('The King of Fighters XIV', 1, 29.99, 'KOF XIV', 0, 15000, @userName, @creationDateTime, null, null, null)
insert into [Warehouse].[Product]
    values ('Street Fighter V', 1, 19.99, 'SF V', 0, 12000, @userName, @creationDateTime, null, null, null)
insert into [Warehouse].[Product]
    values ('Guilty Gear Xrd REV 2', 1, 29.99, 'GG', 0, 11000, @userName, @creationDateTime, null, null, null)
insert into [Warehouse].[Product]
    values ('Tekken 7', 1, 24.99, 'GG', 0, 11000, @userName, @creationDateTime, null, null, null)
insert into [Warehouse].[Product]
    values ('Marvel vs. Campcom: Infinite', 1, 19.99, 'GG', 0, 10000, @userName, @creationDateTime, null, null, null)

insert [Warehouse].[ProductCategory]
    values ('PS4 Arcade Sticks', @userName, getdate(), null, null, null)

insert into [Warehouse].[Product]
    values ('Qanba Dragon Arcade Stick', 2, 260.00, 'Qanba Dragon', 0, 1000, @userName, @creationDateTime, null, null, null)

insert into [Warehouse].[Product]
    values ('Hori Edge PS4', 2, 199.00, 'Hori Edge PS4', 0, 1000, @userName, @creationDateTime, null, null, null)

insert [Warehouse].[ProductCategory]
    values ('Arcade Accesories', @userName, getdate(), null, null, null)

insert into [Warehouse].[Product]
    values ('Qanba Guardian', 3, 75.00, 'Qanba Guardian', 0, 300, @userName, @creationDateTime, null, null, null)

insert into [Warehouse].[Product]
    values ('Qanba Aegis', 3, 100.00, 'Qanba Aegis', 0, 300, @userName, @creationDateTime, null, null, null)

insert into [Warehouse].[Location]
    values ('W0001', 'Warehouse Location 0001', @userName, @creationDateTime, null, null, null)

insert into [Warehouse].[Location]
    values ('W0002', 'Warehouse Location 0002', @userName, @creationDateTime, null, null, null)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (1, 'W0001', 150000, @userName, @creationDateTime)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (2, 'W0002', 120000, @userName, @creationDateTime)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (3, 'W0001', 100000, @userName, @creationDateTime)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (4, 'W0002', 300000, @userName, @creationDateTime)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (5, 'W0001', 400000, @userName, @creationDateTime)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (6, 'W0001', 1000, @userName, @creationDateTime)

insert into [Warehouse].[ProductInventory]
    ([ProductID], [LocationID], [Quantity], [CreationUser], [CreationDateTime])
values
    (7, 'W0001', 1000, @userName, @creationDateTime)

insert into [Sales].[Customer]
    values ('Best Buy', 'Colleen Dunn', @userName, @creationDateTime, null, null, null)

    insert into [Sales].[CustomerAddress]
        values (1, '16-County Metro Area', '#110', 'Minneapolis–Saint Paul', 'Mississippi', '55101', 1, '32088446622', @userName, @creationDateTime, null, null, null)

insert into [Sales].[Customer]
    values ('Circuit City', 'Bill McCorey', @userName, @creationDateTime, null, null, null)
insert into [Sales].[Customer]
    values ('Game Stop', 'Michael Cooper', @userName, @creationDateTime, null, null, null)
insert into [Sales].[Customer]
    values ('Fry''s Electronics', 'John Fry', @userName, @creationDateTime, null, null, null)

insert into [Sales].[Shipper]
    values ('DHL', 'Ricardo A. Bartra', @userName, @creationDateTime, null, null, null)
insert into [Sales].[Shipper]
    values ('FedEx', 'Rob Carter', @userName, @creationDateTime, null, null, null)
insert into [Sales].[Shipper]
    values ('UPS', 'Juan R. Perez', @userName, @creationDateTime, null, null, null)

insert into [Sales].[OrderStatus]
    values (100, 'Created', @userName, @creationDateTime, null, null, null)
insert into [Sales].[OrderStatus]
    values (200, 'Acepted', @userName, @creationDateTime, null, null, null)
insert into [Sales].[OrderStatus]
    values (300, 'Shipped', @userName, @creationDateTime, null, null, null)
insert into [Sales].[OrderStatus]
    values (400, 'Delivered', @userName, @creationDateTime, null, null, null)

insert into [Sales].[PaymentMethod]
    values('7671A4F7-A735-4CB7-AAB4-CF47AE20171D', 'Credit Card', 'Payment with credit card', @userName, @creationDateTime, null, null, null)
insert into [Sales].[PaymentMethod]
    values('F9439225-18BD-4654-AA50-156789BE8B0B', 'Debit Card', 'Payment with debit card', @userName, @creationDateTime, null, null, null)
go
