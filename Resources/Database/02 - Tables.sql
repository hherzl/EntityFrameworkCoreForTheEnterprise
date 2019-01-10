create table [dbo].[EventLog]
(
	[EventLogID] uniqueidentifier not null,
	[EventType] int not null,
	[Key] varchar(255) not null,
	[Message] varchar(max) not null,
	[EntryDate] datetime not null
)

create table [dbo].[ChangeLog]
(
	[ChangeLogID] int not null identity(1, 1),
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
	[ChangeLogExclusionID] int not null identity(1, 1),
	[EntityName] varchar(128) not null,
	[PropertyName] varchar(128) not null
)

create table [dbo].[Country]
(
	[CountryID] int not null,
	[CountryName] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [dbo].[Currency]
(
	[CurrencyID] varchar(10) not null,
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
	[CountryCurrencyID] int not null identity(1, 1),
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
	[EmployeeID] int not null identity(1, 1),
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
	[EmployeeAddressID] int not null identity(1, 1),
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
	[EmployeeEmailID] int not null identity(1, 1),
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
	[ProductCategoryID] int not null identity(1, 1),
	[ProductCategoryName] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[Product]
(
	[ProductID] int not null identity(1, 1),
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

create table [Warehouse].[Location]
(
	[LocationID] varchar(5) not null,
	[LocationName] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[ProductInventory]
(
	[ProductInventoryID] int not null identity(1, 1),
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
	[OrderStatusID] smallint not null,
	[Description] varchar(100) not null,
	[CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[PaymentMethod]
(
	[PaymentMethodID] uniqueidentifier not null,
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
	[CustomerID] int not null identity(1, 1),
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
	[CustomerAddressID] int not null identity(1, 1),
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
	[CustomerEmailID] int not null identity(1, 1),
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
	[ShipperID] int not null identity(1, 1),
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
	[OrderHeaderID] bigint not null identity(1, 1),
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
	[OrderDetailID] bigint not null identity(1, 1),
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
