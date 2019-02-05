# Database

*OnlineStore* database is a model for an online store.

To create database in SQL Server instance, run *deploy.bat* script.

## Schemas

	Dbo
	HumanResources
	Warehouse
	Sales

All tables have primary key with one column.

All tables to save information related to business, have the following columns to audit:

	CreationUser
	CreationDateTime
	LastUpdateUser
	LastUpdateDateTime

Also, there is a concurrency token: *Timestamp* this token allows to perform changes with concurrency.

## Tables

### Dbo Schema

	dbo.ChangeLog
	dbo.ChangeLogExclusion
	dbo.Country
	dbo.Currency
	dbo.CountryCurrency
	dbo.EventLog

### HumanResources Schema

	HumanResources.Employee
	HumanResources.EmployeeAddress
	HumanResources.EmployeeEmail

### Warehouse Schema

	Warehouse.Location
	Warehouse.Product
	Warehouse.ProductCategory
	Warehouse.ProductInventory

### Sales Schema

	Sales.Customer
	Sales.OrderHeader
	Sales.OrderDetail
	Sales.OrderStatus
	Sales.PaymentMethod
	Sales.Shipper

## Scalar Functions

	HumanResources.ufnGetEmployeeFullName
	Warehouse.ufnGetStock

## Table Functions

	Sales.ufnGetCustomerContact

## Views

	HumanResources.EmployeeInfo
	Sales.OrderSummary

## Stored Procedures
