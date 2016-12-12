use Store
go

alter table [EventLog] add constraint EventLog_PK primary key (EventLogID)
go

alter table [HumanResources].[Employee] add constraint HumanResources_Employee_PK primary key (EmployeeID)
go

alter table [Production].[ProductCategory] add constraint Production_ProductCategory_PK primary key (ProductCategoryID)
go

alter table [Production].[Product] add constraint Production_Product_PK primary key (ProductID)
go

alter table [Production].[ProductInventory] add constraint Production_ProductInventory_PK primary key (ProductInventoryID)
go

alter table [Sales].[Customer] add constraint Sales_Customer_PK primary key (CustomerID)
go

alter table [Sales].[Shipper] add constraint Sales_Shipper_PK primary key (ShipperID)
go

alter table [Sales].[Order] add constraint Sales_Order_PK primary key (OrderID)
go

alter table [Sales].[OrderDetail] add constraint Sales_OrderDetail_PK primary key (OrderID, ProductID)
go
