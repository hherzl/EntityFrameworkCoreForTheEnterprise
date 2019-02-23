create view [HumanResources].[EmployeeInfo]
as
	select
		Employee.EmployeeID,
		HumanResources.ufnGetEmployeeFullName(Employee.EmployeeID) as EmployeeName,
		(select count(EmployeeID) from HumanResources.EmployeeAddress EAddress where EAddress.EmployeeID = Employee.EmployeeID) as EmployeeAddresses,
		(select count(EmployeeID) from HumanResources.EmployeeEmail EEmail where EEmail.EmployeeID = Employee.EmployeeID) as EmployeeEmails
	from
		HumanResources.Employee Employee
go

create view [Sales].[OrderSummary]
as
	select
		OrderHeader.OrderHeaderID,
		OrderHeader.OrderStatusID,
		Customer.CustomerID as CustomerID,
		Customer.CompanyName as CustomerName,
		Employee.EmployeeID as EmployeeID,
		HumanResources.ufnGetEmployeeFullName(OrderHeader.EmployeeID) as EmployeeName,
		Shipper.ShipperID as ShipperID,
		Shipper.CompanyName as ShipperName,
		OrderHeader.OrderDate,
		OrderHeader.Total,
		Currency.CurrencyName,
		PaymentMethod.PaymentMethodName,
		OrderHeader.DetailsCount
	from
		Sales.OrderHeader OrderHeader
		inner join Sales.OrderStatus OrderStatus
			on OrderHeader.OrderStatusID = OrderStatus.OrderStatusID
		inner join Sales.Customer Customer
			on OrderHeader.CustomerID = Customer.CustomerID
		inner join HumanResources.Employee Employee
			on OrderHeader.EmployeeID = Employee.EmployeeID
		inner join Sales.Shipper Shipper
			on OrderHeader.ShipperID = Shipper.ShipperID
		inner join dbo.Currency
			on OrderHeader.CurrencyID = Currency.CurrencyID
		inner join Sales.PaymentMethod PaymentMethod
			on OrderHeader.PaymentMethodID = PaymentMethod.PaymentMethodID
go
