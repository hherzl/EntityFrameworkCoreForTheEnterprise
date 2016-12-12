use Store
go

create table [EventLog]
(
	[EventLogID] int not null identity(1, 1),
	[EventType] int not null,
	[Key] varchar(255) not null,
	[Message] varchar not null,
	[EntryDate] datetime not null
)

create table [HumanResources].[Employee]
(
	[EmployeeID] int not null identity(1, 1),
	[FirstName] varchar(25) not null,
	[MiddleName] varchar(25) null,
	[LastName] varchar(25) not null,
	[BirthDate] datetime not null
)

create table [Production].[ProductCategory]
(
	[ProductCategoryID] int not null identity(1, 1),
	[ProductCategoryName] varchar(100) not null
)

create table [Production].[Product]
(
	[ProductID] int not null identity(1, 1),
	[ProductName] varchar(100) not null,
	[ProductCategoryID] int not null,
	[Description] varchar(255) null
)

create table [Production].[ProductInventory]
(
	[ProductInventoryID] int not null identity(1, 1),
	[ProductID] int not null,
	[EntryDate] datetime not null,
	[Quantity] int not null
)

create table [Sales].[Customer]
(
	[CustomerID] int not null identity(1, 1),
	[CompanyName] varchar(100) null,
	[ContactName] varchar(100) null
)

create table [Sales].[Shipper]
(
	[ShipperID] int not null identity(1, 1),
	[CompanyName] varchar(100) null,
	[ContactName] varchar(100) null
)

create table [Sales].[Order]
(
	[OrderID] int not null identity(1, 1),
	[OrderDate] datetime not null,
	[CustomerID] int not null,
	[EmployeeID] int not null,
	[ShipperID] int not null,
	[Comments] varchar(255) null
)

create table [Sales].[OrderDetail]
(
	[OrderID] int not null,
	[ProductID] int not null,
	[ProductName] varchar(255) not null,
	[UnitPrice] decimal(8, 4) not null,
	[Quantity] int not null,
	[Total] decimal(8, 4) not null
)
go
