# Entity Framework Core For Enterprise

## Introduction

Design an enterpise architecture for applications it's a big challenge, there is a common question in this point: What is the the best way to solve this issue following the best practices according to selected technology in our company.

This guide uses .Net Core, so We'll work with Entity Framework Core, but these concepts apply for another technologies like Dapper or another ORM.

In fact, we'll take a look at the common requirements to design of enterprise architect in this article.

The sample database provided in this guide represents an online store.

Because we're working with Entity Framework Core and ASP.NET Core, unit tests using in memory database provider; integration tests using a Test Web Server.

All tests (units and integration) are writen with xUnit framework.

## Background

According to my experience, enterprise architecture for applications should have the following levels:

* Entity Layer: Contains entities (POCOs)
* Data Layer: Contains objects related to database access
* Business Layer: Contains definitions and validations related to business
* External Services Layer (optional): Contains invocations for external services (ASMX, WCF, RESTful)
* Common: Contains common objects for layers (e.g. Loggers, Mappers, Extensions)
* Tests (QA): Contains tests for back-end (units and integration)
* Presentation Layer: This is the UI
* UI Tests (QA): Contains automated tests for front-end

Architecture: Big Picture

|Layer|Technologies|Level|
|-----|------------|-----|
|DATABASE|SQL Server|DATABASE|
|ENTITY LAYER|POCOs|Backend|
|DATA LAYER|DbContext, Configurations, Contracts, Data Contracts and Repositories|Backend|
|BUSINESS LAYER|Services, Contracts, DataContracts, Exceptions and Loggers|Backend|
|EXTERNAL SERVICES LAYER|ASMX, WCF, RESTful|Backend|
|COMMON|Loggers, Mappers, Extensions|Backend|
|PRESENTATION LAYER|UI Frameworks (AngularJS or ReactJS or Vue.js or Anothers)|Frontend|
|USER||Clients|

## Skills Prerequisites

Before to continuing, keep in mind we need to have the folllowing skills in order to understand this guide:

* OOP (Object Oriented Programming)
* AOP (Aspect Oriented Programming)
* ORM (Object Relational Mapping)
* Design Patterns: Domain Driven Design, Repository & Unit of Work and IoC

## Software Prerequisites

* .Net Core
* Visual Studio 2017
* SQL Server instance (local or remote)
* SQL Server Management Studio

## Code

### Chapter 01 - Database

Take a look for sample database to understand each component in architecture. In this database there are 4 schemas: Dbo, HumanResources, Production and Sales.

Each schema represents a division on store company, keep this in mind because all code is designed following this aspect; at this moment this code only implements features for Production and Sales schemas.

All tables have a primary key with one column and have columns for creation, last update and concurrency token.

Take a look on database scripts.

### Chapter 02 - Core Project

Core project represents the core for solution, in this guide Core project includes entity, data and business layers.

We're working with .NET Core, the naming convention is .NET naming convention, so it's very useful to define a naming convention table to show how to set names in code, something like this:

This convention is important because it defines the naming guidelines for architecture.

This is the structure for Store.Core project:

* EntityLayer
* DataLayer
* DataLayer\Contracts
* DataLayer\DataContracts
* DataLayer\Mapping
* DataLayer\Repositories
* BusinessLayer
* BusinessLayer\Contracts
* BusinessLayer\Responses

Inside of Entitylayer, we'll place all entities, in this context, entity means a class that represents a table or view from database, sometimes entity is named POCO (Plain Old Common language runtime Object) than means a class with only properties not methods nor other things (events); according to wkempf feedback it's necessary to be clear about POCOs, POCOs can have methods and events and other members but it's not common to add those members in POCOs.

Inside of DataLayer, we'll place DbContext because it's a common class for DataLayer.

For of DataLayer\Contracts, we'll place all interfaces that represent operations catalog, we're focusing on schemas and we'll create one interface per schema and Store contract for default schema (dbo).

For DataLayer\DataContracts, we'll place all object definitions for returned values from Contracts namespace, for now this directory contains OrderInfo class definition.

For DataLayer\Mapping, we'll place all object definitions related to mapping classes for database.

For DataLayer\Repositories, we'll place the implementations for Contracts definitons.

One repository includes operations related to one schema, so we have 4 repositories: DboRepository, HumanResourcesRepository, ProductionRepository and SalesRepository.

Inside of EntityLayer and DataLayer\Mapping, we'll create one directory per schema.

Inside of BusinessLayer, we'll create the interfaces and implementations for services, in this case, the services will contain the methods according to use cases (or something similar) and those methods must perform validations and handle exceptions related to busines.

For BusinessLayer\Responses, we'll create the responses: single, list and paged to represent the result from services.

We'll inspect the code to understand these concepts but the review would be with one object per level because the remaining code is similar.

### Entity Layer

Please take a look at POCOs, we're using nullable types instead of native types because nullable are easy to evaluate if property has value or not, that's more similar to database model.

In EntityLayer there are two interfaces: IEntity and IAuditEntity, IEntity represents all entities in our application and IAuditEntity represents all entities that allows to save audit information: create and last update; as special point if we have mapping for views, those classes do not implement IAuditEntity because a view doesn't allow insert, update and elete operations.

### Data Layer

For this source code, the implementation for repositories are by feature instead of generic repositories; the generic repositories require to create derived repositories in case we need to implement specific operations. I prefer repositories by features because do not require to create derived objects (interfaces and classes) but a repository by feature will contains a lot of operations because is a placeholder for all operations in feature.

The sample database for this article contains 4 schemas in database, so we'll have 4 repositories, this implementation provides a separation of concepts.

We are working with EF Core in this guide, so we need to have a DbContext and objects that allow mapping classes and database objects (tables and views).

Repository versus DbHelper versus Data Access Object

This issue is related to naming objects, some years ago I used DataAccessObject as suffix to class that contain database operatios (select, insert, update, delete, etc). Other developers used DbHelper as suffix to represent this kind of objects, at my beggining in EF I learned about repository design pattern, so from my point of view I prefer to use Repository suffix to name the object that contains database operations.

How about Unit of Work? in EF 6.x was usually create a repository class and unit of work class: repository provided operations for database access and unit of work provided operations to save changes in database; but in EF Core it's a common practice to have only repositories and no unit of work; anyway for this code we have added two methods in Repository class: CommitChanges and CommitChangesAsync, so just to make sure that inside of all data writing mehotds in repositories call CommitChanges or CommitChangesAsync and with that design we have two definitions working on our architecture.

On DbContext for this version, we're using DbSet on the fly instead of declaring DbSet properties in DbContext. I think that it's more about architect preferences I prefer to use on the fly DbSet because I don't worry about adding all DbSets to DbContext but this style would be changed if you considered it's more accurate to use declarated DbSet properties in DbContext.

How about async operations? In previous versions of this post I said we'll implement async operations in the last level: REST API, but I was wrong about that because .NET Core it's more about async programming, so the best decision is handle all database operations in async way using the Async methods that EF Core provides.

We can take a look on Repository class, there are two methods: Add and Update, for this example Order class has audit properties: CreationUser, CreationDateTime, LastUpdateUser and LastUpdateDateTime also Order class implements IAuditEntity interface, that interface is used to set values for audit properties

For the current version of this article, we going to omit the services layer but in some cases, there is a layer that includes the connection for external services (ASMX, WCF and RESTful).

Stored Procedures versus LINQ Queries

In data layer, there is a very interesting point: How we can use stored procedures? For the current version of EF Core, there isn't support for stored procedures, so we can't use them in a native way, inside of DbSet, there is a method to execute a query but that works for stored procedures not return a result set (columns), we can add some extension methods and add packages to use classic ADO.NET, so in that case we need to handle the dynamic creation of objects to represent the stored procedure result; that makes sense? if we consume a procedure with name GetOrdersByMonth and that procedure returns a select with 7 columns, to handle all results in the same way, we'll need to define objects to represent those results, that objects must define inside of DataLayer\DataContracts namespace according to our naming convention.

Inside of enterprise environment, a common discussion is about LINQ queries or stored procedures. According to my experience, I think the best way to solve that question is: review design conventions with architect and database administrator; nowadays, it's more common to use LINQ queries in async mode instead of stored procedures but sometimes some companies have restrict conventions and do not allow to use LINQ queries, so it's required to use stored procedure and we need to make our architecture flexible because we don't say to developer manager "the business logic will be rewrite because Entity Framework Core doesn't allow to invoke stored procedures"

As we can see until now, assuming we have the extension methods for EF Core to invoke stored procedures and data contracts to represent results from stored procedures invocations, Where do we place those methods? It's preferable to use the same convention so we'll add those methods inside of contracts and repositories; just to be clear if we have procedures named Sales.GetCustomerOrdersHistory and HumanResources.DisableEmployee; we must to place methods inside of Sales and HumanResources repositories.

Just to be clear: STAY AWAY FROM STORED PROCEDURES!

The previous concept applies in the same way for views in database. In addition, we only need to check that repositories do not allow add, update and delete operations for views.

Change Tracking: inside of Repository class there is a method with name GetChanges, that method get all changes from DbContext through ChangeTracker and returns all changes, so those values are saved in ChangeLog table in CommitChanges method. You can update one existing entity with business object, later you can check your ChangeLog table:

### Business Layer

Controller versus Service versus Business Object

There is a common issue in this point, How we must to name the object that represents business operations: for first versions of this article I named this object as BusinessObject, that can be confusing for some developers, some developers do not name this as business object because the controller in Web API represents business logic, but Service is another name used by developers, so from my point of view is more clear to use Service as sufix for this object. If we have a Web API that implements business logic in controller we can ommit to have services, but if there is business layer it is more useful to have services, these classes must to implement logic business and controllers must invoke service's methods.

Business Layer: Handle Related Aspects To Business

* Logging: we need to have a logger object, that means an object that logs on text file, database, email, etc. all events in our architecture; we can create our own logger implementation or choose an existing log. We have added logging with package Microsoft.Extensions.Logging, in this way we're using the default log system in .NET Core, we can use another log mechanism but at this moment we'll use this logger, inside of every method in controllers and business objects, there is a code line like this: Logger?.LogInformation("{0} has been invoked", nameof(GetOrdersAsync));, in this way we make sure invoke logger if is a valid instance and ths using of nameof operator to retrieve the name of member without use magic strings, after we'll add code to save all logs into database.
* Business exceptions: The best way to handle messaging to user is with custom exceptions, inside of business layer, we'll add definitions for exceptions to represent all handle errors in architecture.
* Transactions: as we can see inside of Sales business object, we have implemented transaction to handle multiple changes in our database; inside of CreateOrderAsync method, we invoke methods from repositories, inside of repositories we don't have any transactions because the service is the responsible for transactional process, also we added logic to handle exceptions related to business with custom messages because we need to provide a friendly message to the end-user.
There is a CloneOrderAsync method, this method provides a copy from existing order, this is a common requirement on ERP because it's more easy create a new order but adding some modifications instead of create the whole order there are cases where the sales agent create a new order but removing 1 or 2 lines from details or adding 1 or 2 details, anyway never let to front-end developer to add this logic in UI, the API must to provide this feature.
GetCreateOrderRequestAsync method in SalesRepository provides the required information to create an order, information from foreign keys: products and anothers. With this method we are providing a model that contains the list for foreign keys and in that way we reduce the work from front-end to know how to create create order operation.

In BusinessLayer it's better to have custom exceptions for represent errors instead of send simple string messages to client, obviously the custom exception must have a message but in logger there will be a reference about custom exception. For this architecture these are the custom exceptions:

Business Exceptions

|Name|Description|
|----|-----------|
|AddOrderWithDiscontinuedProductException|Represents an exception adding order with a discontinued product|
|ForeignKeyDependencyException|Represents an exception deleting an order with detail rows|
|DuplicatedProductNameException|Represents an exception adding product with existing name|
|NonExistingProductException|Represents an exception adding order with non existing product|

### Chapter 03 - Putting All Code Together

This is an example of how we can retrieve a list of orders list:

```csharp
// Create logger instance
var logger = LoggerMocker.GetLogger<ISalesService>();

// Create application user
var userInfo = new UserInfo("admin");

// Create options for DbContext
var options = new DbContextOptionsBuilder<StoreDbContext>()
    .UseSqlServer("YourConnectionStringHere")
    .Options;

// Create instance of business object
// Set logger, application user and context for database
using (var service = new SalesService(logger, userInfo, new StoreDbContext(options)))
{
	// Declare parameters and set values for paging
	var pageSize = 10;
	var pageNumber = 1;

	// Get response from business object
	var response = await service.GetOrdersAsync(pageSize, pageNumber);

	// Validate if there was an error
	var valid = !response.DidError;
}
```

As we can see, GetOrdersAsync method in SalesService retrieves rows from Sales.Order table as a generic list.
Get by Key

This is an example of how we can retrieve an entity by key:

```csharp
// Create logger instance
var logger = LoggerMocker.GetLogger<ISalesService>();

// Create application user
var userInfo = new UserInfo("admin");

// Create options for DbContext
var options = new DbContextOptionsBuilder<StoreDbContext>()
    .UseSqlServer("YourConnectionStringHere")
    .Options;

// Create instance of business object
// Set logger, application user and context for database
using (var service = new SalesService(logger, userInfo, new StoreDbContext(options)))
{
	// Declare parameters and set values for paging
	var id = 1;

	// Get response from business object
	var response = await service.GetOrderAsync(id);

	// Validate if there was an error
	var valid = !response.DidError;
	
	// Get entity
	var entity = response.Model;
}
```

For incoming versions of this article, there will be samples for another operations.

### Chapter 04 - Mocker

Mocker it's a project that allows to create rows in Sales.Order, Sales.OrderDetail and Production.ProductInventory tables for a range of dates, by default Mocker creates rows for one year.

Now in the same window terminal, we need to run the following command: dotnet run and if everything works fine, we can check in our database the data for Order, OrderDetail and ProductInventory tables.

How Mocker works? set a range for dates and a limit of orders per day, then iterates all days in date range except sundays beacuse we're assuming create order process is not allowed on sundays; then create the instance of DbContext and Services, arranges data using a random index to get elements from products, customers, currencies and payment methods; then invokes the CreateOrderAsync method.

You can adjust the range for dates and orders per day to mock data according to your requirements, once the Mocker has finished you can check the data on your database.

### Chapter 05 - Web API

There is a project with name Store.API, this represents Web API for thissolution, this project has references to Store.Core project.

ViewModel versus Request

ViewModel is an object that contains behavior, request is the action related to invoke a Web API method, this is the misunderstood: ViewModel is an object linked to a view, contains behavior to handle changes and sync up with view; usually the parameter for Web API method is an object with properties, so this definition is named Request; MVC is not MVVM, the life's cycle for model is different in those patterns, this definition doesn't keep state between UI and API, also the process to set properties values in request from query string is handled by a model binder.

This class it's the configuration point for Web API project, in this class there is the configuration for dependency injection, API's configuration and another settings.

For Web API project, these are the routes for controllers:

|Verb|Route|Description|
|----|-----|-----------|
|GET|api/v1/Sales/Order|Get orders|
|GET|api/v1/Sales/Order/1|Get order by id|
|GET|api/v1/Sales/CreateOrderRequest|Get model to create order|
|GET|api/v1/Sales/CloneOrder/3|Clone an existing order|
|POST|api/v1/Sales/Order|Create a new order|
|DELETE|api/v1/Sales/Order|Delete an existing order|

As we can see there is a v1 in each route, this is because the version for Web API is 1 and that value is defined in Route attribute for controllers in Web API project.

### Chapter 06 - Unit Tests for Web API

Now we proceed to add unit tests for Web API project, these tests work with in memory database, what is the difference between unit tests and integration tests? for unit tests we simulate all dependencies for Web API project and for integration tests we run a process that simulates Web API execution. I mean a simulation of Web API (accept Http requests), obviously there is more information about unit tests and integration tests but at this point this basic idea is enough.

What is TDD? Testing is important in these days, because with unit tests it's easy to performing tests for features before to publish, Test Driven Development (TDD) is the way to define unit tests and validate the behavior in code. Another concept in TDD is AAA: Arrange, Act and Assert; arrange is the block for creation of objects, act is the block to place all invocations for methods and assert is the block to validate the results from methods invocation.

As we can see those methods perform tests for Urls in Web API project, please take care about the tests are async methods.

Don't forget we can have more tests, we have class with name ProductionTests to perform requests for ProductionController.

### Chapter 07 - Integration Tests for Web API

In order to work with integration tests, we need to create a class to provide a Web Host to performing Http behavior, this class it will be TestFixture and to represent Http requests for Web API, there is a class with name SalesTests, this class will contains all requests for defined actions in SalesController class, but using a mocked Http client.

## Code Improvements

* Add Security (e.g. IdentityServer4)
* Save logs to text file or database
* Implement Money Pattern to represent money in application
* Add section to explain why this code doesn't use Generic Repository and Unit of Work

## Points of Interest

* In this article, we're working with Entity Framework Core.
* Entity Framework Core has in memory database.
* We can adjust all repositories to expose specific operations, in some cases we don't want to have GetAll, Add, Update or Delete operations.
* Unit tests perform testing for Assemblies.
* Integration tests perform testing for Web Server.
* Mocker is an object that creates an instance of object in testing.

For more information, please check: [`Entity Framework Core 2 for Enterprise`](https://www.codeproject.com/Articles/1160586/Entity-Framework-Core-for-Enterprise)
