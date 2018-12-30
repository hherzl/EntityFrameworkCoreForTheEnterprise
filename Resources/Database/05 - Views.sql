create view [HumanResources].[EmployeeInfo]
as
	select
		Employee.FirstName + ' ' + isnull(Employee.MiddleName + ' ', '') + Employee.LastName as EmployeeName,
		(select count(EmployeeID) from HumanResources.EmployeeAddress where EmployeeID = Employee.EmployeeID) as EmployeeAddresses,
		(select count(EmployeeID) from HumanResources.EmployeeEmail where EmployeeID = Employee.EmployeeID) as EmployeeEmails
	from
		HumanResources.Employee Employee
go

create view [Sales].[OrderSummary]
as
	select
		OrderHeader.OrderHeaderID,
		Customer.CompanyName as CustomerName,
		HumanResources.ufnGetEmployeeFullName(OrderHeader.EmployeeID) as EmployeeName,
		Shipper.CompanyName as ShipperName,
		OrderHeader.OrderDate,
		OrderHeader.Total,
		Currency.CurrencyName,
		PaymentMethod.PaymentMethodName,
		OrderHeader.DetailsCount
	from
		Sales.OrderHeader OrderHeader
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
