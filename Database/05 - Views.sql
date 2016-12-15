create view OrderSummary
as
	select
		OrderHeader.OrderID,
		OrderHeader.OrderDate,
		Customer.CompanyName as CustomerName,
		Employee.FirstName + ' ' + isnull(Employee.MiddleName, '') + ' ' + Employee.LastName as EmployeeName,
		Shipper.CompanyName as ShipperName
	from
		Sales.[Order] OrderHeader
		inner join Sales.Customer Customer on OrderHeader.CustomerID = Customer.CustomerID
		inner join HumanResources.Employee Employee on OrderHeader.EmployeeID = Employee.EmployeeID
		inner join Sales.Shipper Shipper on OrderHeader.ShipperID = Shipper.ShipperID
go
