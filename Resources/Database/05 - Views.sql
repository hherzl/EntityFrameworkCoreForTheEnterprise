create view [HumanResources].[EmployeeInfo]
as
	select
		Employee.ID,
		HumanResources.ufnGetEmployeeFullName(Employee.ID) as EmployeeName,
		(select count([ID]) from [HumanResources].[EmployeeAddress] EAddress where EAddress.EmployeeID = Employee.ID) as EmployeeAddresses,
		(select count([ID]) from [HumanResources].[EmployeeEmail] EEmail where EEmail.EmployeeID = Employee.ID) as EmployeeEmails
	from
		HumanResources.Employee Employee
go

create view [Sales].[OrderSummary]
as
	select
		OrderHeader.ID OrderID,
		OrderHeader.OrderStatusID,
		Customer.ID as CustomerID,
		Customer.CompanyName as CustomerName,
		Employee.ID as EmployeeID,
		HumanResources.ufnGetEmployeeFullName(OrderHeader.EmployeeID) as EmployeeName,
		Shipper.ID as ShipperID,
		Shipper.CompanyName as ShipperName,
		OrderHeader.OrderDate,
		OrderHeader.Total,
		Currency.CurrencyName,
		PaymentMethod.PaymentMethodName,
		OrderHeader.DetailsCount
	from
		Sales.OrderHeader OrderHeader
		inner join Sales.OrderStatus OrderStatus
			on OrderHeader.OrderStatusID = OrderStatus.ID
		inner join Sales.Customer Customer
			on OrderHeader.CustomerID = Customer.ID
		inner join HumanResources.Employee Employee
			on OrderHeader.EmployeeID = Employee.ID
		inner join Sales.Shipper Shipper
			on OrderHeader.ShipperID = Shipper.ID
		inner join dbo.Currency
			on OrderHeader.CurrencyID = Currency.ID
		inner join Sales.PaymentMethod PaymentMethod
			on OrderHeader.PaymentMethodID = PaymentMethod.ID
go
