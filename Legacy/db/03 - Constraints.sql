alter table [dbo].[EventLog]
	add constraint [PK_EventLog] primary key (EventLogID)
go

alter table [dbo].[ChangeLog]
	add constraint [PK_ChangeLog] primary key (ChangeLogID)
go

alter table [dbo].[ChangeLogExclusion]
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

alter table [HumanResources].[EmployeeAddress]
	add constraint [PK_HumanResources_EmployeeAddress] primary key (EmployeeAddressID)
go

alter table [HumanResources].[EmployeeEmail]
	add constraint [PK_HumanResources_EmployeeEmail] primary key (EmployeeEmailID)
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

alter table [Sales].[CustomerAddress]
	add constraint [PK_Sales_CustomerAddress] primary key (CustomerAddressID)
go

alter table [Sales].[CustomerEmail]
	add constraint [PK_Sales_CustomerEmail] primary key (CustomerEmailID)
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

alter table [dbo].[CountryCurrency]
	add constraint [FK_CountryCurrency_Country] foreign key (CountryID)
		references [dbo].[Country]
go

alter table [dbo].[CountryCurrency]
	add constraint [FK_CountryCurrency_Currency] foreign key (CurrencyID)
		references [dbo].[Currency]
go

alter table [Production].[Product]
	add constraint [FK_Production_Product_ProductCategory] foreign key (ProductCategoryID)
		references [Production].[ProductCategory]
go

alter table [Production].[ProductInventory]
	add constraint [FK_Production_ProductInventory_Product] foreign key (ProductID)
		references [Production].[Product]
go

alter table [Production].[ProductInventory]
	add constraint [FK_Production_ProductInventory_Warehouse] foreign key (WarehouseID)
		references [Production].[Warehouse]
go

alter table [HumanResources].[EmployeeAddress]
	add constraint [FK_HumanResources_EmployeeAddress] foreign key (EmployeeID)
		references [HumanResources].[Employee]
go

alter table [HumanResources].[EmployeeEmail]
	add constraint [FK_HumanResources_EmployeeEmail] foreign key (EmployeeID)
		references [HumanResources].[Employee]
go

alter table [Sales].[CustomerAddress]
	add constraint [FK_Sales_CustomerAddress] foreign key (CustomerID)
		references [Sales].[Customer]
go

alter table [Sales].[CustomerEmail]
	add constraint [FK_Sales_CustomerEmail] foreign key (CustomerID)
		references [Sales].[Customer]
go

alter table [Sales].[Order]
	add constraint [FK_Sales_Order_OrderStatus] foreign key (OrderStatusID)
		references [Sales].[OrderStatus]
go

alter table [Sales].[Order]
	add constraint [FK_Sales_Order_Customer] foreign key (CustomerID)
		references [Sales].[Customer]
go

alter table [Sales].[Order]
	add constraint [FK_Sales_Order_Employee] foreign key (EmployeeID)
		references [HumanResources].[Employee]
go

alter table [Sales].[Order]
	add constraint [FK_Sales_Order_Shipper] foreign key (ShipperID)
		references [Sales].[Shipper]
go

alter table [Sales].[Order]
	add constraint [FK_Sales_Order_Currency] foreign key (CurrencyID)
		references [dbo].[Currency]
go

alter table [Sales].[Order]
	add constraint [FK_Sales_Order_PaymentMethod] foreign key (PaymentMethodID)
		references [Sales].[PaymentMethod]
go

alter table [Sales].[OrderDetail]
	add constraint [FK_Sales_OrderDetail_Order] foreign key (OrderID)
		references [Sales].[Order]
go

alter table [Sales].[OrderDetail]
	add constraint [FK_Sales_OrderDetail_Product] foreign key (ProductID)
		references [Production].[Product]
go
