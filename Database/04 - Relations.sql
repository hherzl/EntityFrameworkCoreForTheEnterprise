alter table [Production].[Product] add constraint Production_Product_ProductCategory foreign key (ProductCategoryID) references [Production].[ProductCategory]
go

alter table [Production].[ProductInventory] add constraint Production_ProductInventory_Product foreign key (ProductID) references [Production].[Product]
go

alter table [Sales].[Order] add constraint Sales_Order_Customer foreign key (CustomerID) references [Sales].[Customer]
go

alter table [Sales].[Order] add constraint Sales_Order_Employee foreign key (EmployeeID) references [HumanResources].[Employee]
go

alter table [Sales].[Order] add constraint Sales_Order_Shipper foreign key (ShipperID) references [Sales].[Shipper]
go

alter table [Sales].[OrderDetail] add constraint Sales_OrderDetail_Order foreign key (OrderID) references [Sales].[Order]
go

alter table [Sales].[OrderDetail] add constraint Sales_OrderDetail_Product foreign key (ProductID) references [Production].[Product]
go
