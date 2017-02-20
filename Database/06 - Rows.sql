declare @userName varchar(25)
select @userName = 'seed'

insert into [HumanResources].[Employee] values ('John', null, 'Doe', getdate(), @userName, getdate(), null, null, null)

insert [Production].[ProductCategory] values ('PS4 Games', @userName, getdate(), null, null, null)

insert into [Production].[Product] values ('King of Fighters XIV', 1, 59.99, 'KOF XIV', 0, @userName, getdate(), null, null, null)
insert into [Production].[Product] values ('Street Fighter V', 1, 49.99, 'SF V', 0, @userName, getdate(), null, null, null)
insert into [Production].[Product] values ('Guilty Gear', 1, 39.99, 'GG', 0, @userName, getdate(), null, null, null)

insert into [Production].[ProductInventory] values (1, 100000, 100000, @userName, getdate(), null, null, null)
insert into [Production].[ProductInventory] values (2, 100000, 100000, @userName, getdate(), null, null, null)

insert into [Sales].[Customer] values ('Best Buy', 'Colleen Dunn', @userName, getdate(), null, null, null)
insert into [Sales].[Customer] values ('Circuit City', 'Bill McCorey', @userName, getdate(), null, null, null)
insert into [Sales].[Customer] values ('Game Stop', 'Michael Cooper', @userName, getdate(), null, null, null)

insert into [Sales].[Shipper] values ('DHL', 'Ricardo A. Bartra', @userName, getdate(), null, null, null)
insert into [Sales].[Shipper] values ('FedEx', 'Rob Carter', @userName, getdate(), null, null, null)
insert into [Sales].[Shipper] values ('UPS', 'Juan R. Perez', @userName, getdate(), null, null, null)

insert into [Sales].[OrderStatus] values (100, 'Created', @userName, getdate(), null, null, null)
go
