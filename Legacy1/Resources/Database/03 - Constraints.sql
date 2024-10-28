alter table [dbo].[EventLog]
	add constraint [PK_EventLog] primary key ([ID])
go

alter table [dbo].[ChangeLog]
	add constraint [PK_ChangeLog] primary key ([ID])
go

alter table [dbo].[ChangeLogExclusion]
	add constraint [PK_ChangeLogExclusion] primary key([ID])

alter table [dbo].[Country]
	add constraint [PK_Country] primary key([ID])
go

alter table [dbo].[Currency]
	add constraint [PK_Currency] primary key([ID])
go

alter table [dbo].[CountryCurrency]
	add constraint [PK_CountryCurrency] primary key([ID])
go

alter table [HumanResources].[Employee]
	add constraint [PK_HumanResources_Employee] primary key ([ID])
go

alter table [HumanResources].[EmployeeAddress]
	add constraint [PK_HumanResources_EmployeeAddress] primary key ([ID])
go

alter table [HumanResources].[EmployeeEmail]
	add constraint [PK_HumanResources_EmployeeEmail] primary key ([ID])
go

alter table [Warehouse].[ProductCategory]
	add constraint [PK_Production_ProductCategory] primary key ([ID])
go

alter table [Warehouse].[Product]
	add constraint [PK_Production_Product] primary key ([ID])
go

alter table [Warehouse].[Product]
	add constraint [U_Production_Product_ProductName] unique ([ProductName])
go

alter table [Warehouse].[ProductUnitPriceHistory]
	add constraint [U_Production_ProductUnitPriceHistory] primary key ([ID])
go

alter table [Warehouse].[ProductInventory]
	add constraint [PK_Production_ProductInventory] primary key ([ID])
go

alter table [Warehouse].[Location]
	add constraint [PK_Warehouse_Location] primary key ([ID])
go

alter table [Sales].[Customer]
	add constraint [PK_Sales_Customer] primary key ([ID])
go

alter table [Sales].[CustomerAddress]
	add constraint [PK_Sales_CustomerAddress] primary key ([ID])
go

alter table [Sales].[CustomerEmail]
	add constraint [PK_Sales_CustomerEmail] primary key ([ID])
go

alter table [Sales].[Shipper]
	add constraint [PK_Sales_Shipper] primary key ([ID])
go

alter table [Sales].[OrderStatus]
	add constraint [PK_Sales_OrderStatus] primary key ([ID])
go

alter table [Sales].[PaymentMethod]
	add constraint [PK_Sales_PaymentMethod] primary key ([ID])
go

alter table [Sales].[OrderHeader]
	add constraint [PK_Sales_OrderHeader] primary key ([ID])
go

alter table [Sales].[OrderDetail]
	add constraint [PK_Sales_OrderDetail] primary key ([ID])
go

alter table [dbo].[CountryCurrency]
	add constraint [U_CountryCurrency] unique ([CountryID], [CurrencyID])
go

alter table [Sales].[OrderDetail]
	add constraint [U_Sales_OrderDetail] unique ([OrderHeaderID], [ProductID])
go

alter table [ChangeLogExclusion]
	add constraint [U_ChangeLogExclusion] unique([EntityName], [PropertyName])
go

alter table [dbo].[CountryCurrency]
	add constraint [FK_CountryCurrency_Country] foreign key ([CountryID])
		references [dbo].[Country]
go

alter table [dbo].[CountryCurrency]
	add constraint [FK_CountryCurrency_Currency] foreign key ([CurrencyID])
		references [dbo].[Currency]
go

alter table [Warehouse].[Product]
	add constraint [FK_Warehouse_Product_ProductCategory] foreign key ([ProductCategoryID])
		references [Warehouse].[ProductCategory]
go

alter table [Warehouse].[ProductUnitPriceHistory]
	add constraint [FK_Warehouse_ProductUnitPriceHistory_Product] foreign key ([ProductID])
		references [Warehouse].[Product]
go

alter table [Warehouse].[ProductInventory]
	add constraint [FK_Warehouse_ProductInventory_Product] foreign key ([ProductID])
		references [Warehouse].[Product]
go

alter table [Warehouse].[ProductInventory]
	add constraint [FK_Warehouse_ProductInventory_Warehouse_Location] foreign key ([LocationID])
		references [Warehouse].[Location]
go

alter table [Warehouse].[ProductInventory]
	add constraint [FK_Warehouse_ProductInventory_Sales_OrderDetail] foreign key ([OrderDetailID])
		references [Sales].[OrderDetail]
go

alter table [HumanResources].[EmployeeAddress]
	add constraint [FK_HumanResources_EmployeeAddress] foreign key ([EmployeeID])
		references [HumanResources].[Employee]
go

alter table [HumanResources].[EmployeeEmail]
	add constraint [FK_HumanResources_EmployeeEmail] foreign key ([EmployeeID])
		references [HumanResources].[Employee]
go

alter table [Sales].[CustomerAddress]
	add constraint [FK_Sales_CustomerAddress] foreign key ([CustomerID])
		references [Sales].[Customer]
go

alter table [Sales].[CustomerEmail]
	add constraint [FK_Sales_CustomerEmail] foreign key ([CustomerID])
		references [Sales].[Customer]
go

alter table [Sales].[OrderHeader]
	add constraint [FK_Sales_OrderHeader_OrderStatus] foreign key ([OrderStatusID])
		references [Sales].[OrderStatus]
go

alter table [Sales].[OrderHeader]
	add constraint [FK_Sales_OrderHeader_Customer] foreign key ([CustomerID])
		references [Sales].[Customer]
go

alter table [Sales].[OrderHeader]
	add constraint [FK_Sales_OrderHeader_Employee] foreign key ([EmployeeID])
		references [HumanResources].[Employee]
go

alter table [Sales].[OrderHeader]
	add constraint [FK_Sales_OrderHeader_Shipper] foreign key ([ShipperID])
		references [Sales].[Shipper]
go

alter table [Sales].[OrderHeader]
	add constraint [FK_Sales_OrderHeader_Currency] foreign key ([CurrencyID])
		references [dbo].[Currency]
go

alter table [Sales].[OrderHeader]
	add constraint [FK_Sales_OrderHeader_PaymentMethod] foreign key ([PaymentMethodID])
		references [Sales].[PaymentMethod]
go

alter table [Sales].[OrderDetail]
	add constraint [FK_Sales_OrderDetail_Order] foreign key ([OrderHeaderID])
		references [Sales].[OrderHeader]
go

alter table [Sales].[OrderDetail]
	add constraint [FK_Sales_OrderDetail_Product] foreign key ([ProductID])
		references [Warehouse].[Product]
go
