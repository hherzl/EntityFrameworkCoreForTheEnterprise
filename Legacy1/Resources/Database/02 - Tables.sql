create table [dbo].[EventLog]
(
	[ID] uniqueidentifier not null,
	[EventType] int not null,
	[Key] varchar(255) not null,
	[Message] varchar(max) not null,
	[EntryDate] datetime not null
)

create table [dbo].[ChangeLog]
(
	[ID] int not null identity(1, 1),
	[ClassName] varchar(255) not null,
	[PropertyName] varchar(255) not null,
	[Key] varchar(255) not null,
	[OriginalValue] varchar(max) null,
	[CurrentValue] varchar(max) null,
	[UserName] varchar(25) not null,
	[ChangeDate] datetime not null
)

create table [dbo].[ChangeLogExclusion]
(
	[ID] int not null identity(1, 1),
	[EntityName] varchar(128) not null,
	[PropertyName] varchar(128) not null
)

create table [dbo].[Country]
(
	[ID] int not null,
	[CountryName] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [dbo].[Currency]
(
	[ID] varchar(10) not null,
	[CurrencyName] varchar(50) not null,
	[CurrencySymbol] varchar(1) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [dbo].[CountryCurrency]
(
	[ID] int not null identity(1, 1),
	[CountryID] int not null,
	[CurrencyID] varchar(10) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [HumanResources].[Employee]
(
	[ID] int not null identity(1, 1),
	[FirstName] varchar(25) not null,
	[MiddleName] varchar(25) null,
	[LastName] varchar(25) not null,
	[BirthDate] datetime not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [HumanResources].[EmployeeAddress]
(
	[ID] int not null identity(1, 1),
	[EmployeeID] int not null,
	[AddressLine1] varchar(50) not null,
	[AddressLine2] varchar(50) null,
	[City] varchar(25) not null,
	[State] varchar(25) not null,
	[ZipCode] varchar(5) null,
	[CountryID] int not null,
	[PhoneNumber] varchar(25) null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [HumanResources].[EmployeeEmail]
(
	[ID] int not null identity(1, 1),
	[EmployeeID] int not null,
	[Email] varchar(50) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[ProductCategory]
(
	[ID] int not null identity(1, 1),
	[ProductCategoryName] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[Product]
(
	[ID] int not null identity(1, 1),
	[ProductName] varchar(100) not null,
	[ProductCategoryID] int not null,
	[UnitPrice] decimal(8, 4) not null,
	[Description] varchar(255) null,
	[Discontinued] bit not null,
	[Stocks] int not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[ProductUnitPriceHistory]
(
	[ID] int not null identity(1, 1),
	[ProductID] int not null,
	[UnitPrice] decimal(8, 4) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[Location]
(
	[ID] varchar(5) not null,
	[LocationName] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[ProductInventory]
(
	[ID] int not null identity(1, 1),
	[ProductID] int not null,
	[LocationID] varchar(5) not null,
	[OrderDetailID] bigint null,
	[Quantity] int not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[OrderStatus]
(
	[ID] smallint not null,
	[Description] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[PaymentMethod]
(
	[ID] uniqueidentifier not null,
	[PaymentMethodName] varchar(50) not null,
	[PaymentMethodDescription] varchar(255) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[Customer]
(
	[ID] int not null identity(1, 1),
	[CompanyName] varchar(100) null,
	[ContactName] varchar(100) null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[CustomerAddress]
(
	[ID] int not null identity(1, 1),
	[CustomerID] int not null,
	[AddressLine1] varchar(50) not null,
	[AddressLine2] varchar(50) null,
	[City] varchar(25) not null,
	[State] varchar(25) not null,
	[ZipCode] varchar(5) null,
	[CountryID] int not null,
	[PhoneNumber] varchar(25) null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[CustomerEmail]
(
	[ID] int not null identity(1, 1),
	[CustomerID] int not null,
	[Email] varchar(50) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[Shipper]
(
	[ID] int not null identity(1, 1),
	[CompanyName] varchar(100) null,
	[ContactName] varchar(100) null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[OrderHeader]
(
	[ID] bigint not null identity(1, 1),
	[OrderStatusID] smallint not null,
	[CustomerID] int not null,
	[EmployeeID] int null,
	[ShipperID] int null,
	[OrderDate] datetime not null,
	[Total] decimal(12, 4) not null,
	[CurrencyID] varchar(10) null,
	[PaymentMethodID] uniqueidentifier null,
	[DetailsCount] int not null,
	[ReferenceOrderID] bigint null,
	[Comments] varchar(max) null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[OrderDetail]
(
	[ID] bigint not null identity(1, 1),
	[OrderHeaderID] bigint not null,
	[ProductID] int not null,
	[ProductName] varchar(255) not null,
	[UnitPrice] decimal(8, 4) not null,
	[Quantity] int not null,
	[Total] decimal(8, 4) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)
go
