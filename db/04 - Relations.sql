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
