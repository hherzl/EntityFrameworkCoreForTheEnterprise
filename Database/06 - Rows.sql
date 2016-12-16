insert into [HumanResources].[Employee] values ('John', null, 'Doe', getdate())
go

insert [Production].[ProductCategory] values ('PS4 Games')
go

insert into [Production].[Product] values ('King of Fighters XIV', 1, 59.99, 'KOF XIV', 0)
insert into [Production].[Product] values ('Street Fighter V', 1, 49.99, 'SF V', 0)
insert into [Production].[Product] values ('Guilty Gear', 1, 39.99, 'GG', 0)
go

insert into [Production].[ProductInventory] values (1, getdate(), 100000)
insert into [Production].[ProductInventory] values (2, getdate(), 100000)
go

insert into [Sales].[Customer] values ('Best Buy', 'Colleen Dunn')
insert into [Sales].[Customer] values ('Circuit City', 'Bill McCorey')
insert into [Sales].[Customer] values ('Game Stop', 'Michael Cooper')
go

insert into [Sales].[Shipper] values ('DHL', 'Ricardo A. Bartra')
insert into [Sales].[Shipper] values ('FedEx', 'Rob Carter')
insert into [Sales].[Shipper] values ('UPS', 'Juan R. Perez')
go
