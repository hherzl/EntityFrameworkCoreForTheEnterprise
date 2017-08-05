alter table [EventLog]
	add constraint [PK_EventLog] primary key (EventLogID)
go

alter table [ChangeLog]
	add constraint [PK_ChangeLog] primary key (ChangeLogID)
go

alter table [ChangeLogExclusion]
	add constraint [PK_ChangeLogExclusion] primary key(ChangeLogExclusionID)

alter table [dbo].[Country]
	add constraint [PK_Country] primary key([CountryID])
go

alter table [dbo].[Currency]
	add constraint [PK_Currency] primary key([CurrencyID])
go

alter table [dbo].[CountryCurrency]
	add constraint [PK_CountryCurrency] primary key([CountryCurrencyID])
go

alter table [HumanResources].[Employee]
	add constraint [PK_HumanResources_Employee] primary key (EmployeeID)
go

alter table [Production].[ProductCategory]
	add constraint [PK_Production_ProductCategory] primary key (ProductCategoryID)
go

alter table [Production].[Product]
	add constraint [PK_Production_Product] primary key (ProductID)
go

alter table [Production].[Product]
	add constraint [U_Production_Product_ProductName] unique (ProductName)
go

alter table [Production].[ProductInventory]
	add constraint [PK_Production_ProductInventory] primary key (ProductInventoryID)
go

alter table [Production].[Warehouse]
	add constraint [PK_Production_Warehouse] primary key (WarehouseID)
go

alter table [Sales].[Customer]
	add constraint [PK_Sales_Customer] primary key (CustomerID)
go

alter table [Sales].[Shipper]
	add constraint [PK_Sales_Shipper] primary key (ShipperID)
go

alter table [Sales].[OrderStatus]
	add constraint [PK_Sales_OrderStatus] primary key (OrderStatusID)
go

alter table [Sales].[PaymentMethod]
	add constraint [PK_Sales_PaymentMethod] primary key (PaymentMethodID)
go

alter table [Sales].[Order]
	add constraint [PK_Sales_Order] primary key (OrderID)
go

alter table [Sales].[OrderDetail]
	add constraint [PK_Sales_OrderDetail] primary key (OrderDetailID)
go

alter table [dbo].[CountryCurrency]
	add constraint [U_CountryCurrency] unique (CountryID, CurrencyID)
go

alter table [Sales].[OrderDetail]
	add constraint [U_Sales_OrderDetail] unique (OrderID, ProductID)
go

alter table [ChangeLogExclusion]
	add constraint [U_ChangeLogExclusion] unique(EntityName, PropertyName)
go
