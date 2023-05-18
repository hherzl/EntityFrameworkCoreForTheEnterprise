using System.Dynamic;
using CatFactory.EntityFrameworkCore;
using CatFactory.ObjectRelationalMapping;
using CatFactory.SqlServer;
using CatFactory.SqlServer.CodeFactory;
using CatFactory.SqlServer.DatabaseObjectModel;
using CatFactory.SqlServer.ObjectRelationalMapping;

var db = SqlServerDatabase.CreateWithDefaults("RothschildHouse");

db.AddDefaultTypeMapFor(typeof(string), "nvarchar");
db.AddDefaultTypeMapFor(typeof(DateTime), "datetime");

var enumDescription = db
    .DefineEntity(new
    {
        Id = (short)0,
        Type = "",
        Value = (long)0,
        Description = ""
    })
    .SetNaming("EnumDescription")
    .SetColumnFor(e => e.Type, length: 511)
    .SetColumnFor(e => e.Description, length: 200)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    ;

var country = db
    .DefineEntity(new
    {
        Id = (short)0,
        Name = "",
        TwoLetterIsoCode = "",
        ThreeLetterIsoCode = ""
    })
    .SetNaming("Country")
    .SetColumnFor(e => e.Name, length: 100)
    .SetColumnFor(e => e.TwoLetterIsoCode, length: 2)
    .SetColumnFor(e => e.ThreeLetterIsoCode, length: 3)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    .AddUnique(e => e.TwoLetterIsoCode)
    .AddUnique(e => e.ThreeLetterIsoCode)
    ;

var bank = db
    .DefineEntity(new
    {
        Id = (short)0,
        Name = ""
    })
    .SetNaming("Bank")
    .SetColumnFor(e => e.Name, length: 100)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    ;

var person = db
    .DefineEntity(new
    {
        Id = 0,
        GivenName = "",
        MiddleName = "",
        FamilyName = "",
        FullName = "",
        BirthDate = DateTime.Now,
        Gender = ""
    })
    .SetNaming("Person")
    .SetColumnFor(e => e.GivenName, length: 25)
    .SetColumnFor(e => e.MiddleName, length: 25, nullable: true)
    .SetColumnFor(e => e.FamilyName, length: 25)
    .SetColumnFor(e => e.FullName, length: 75)
    .SetColumnFor(e => e.BirthDate, nullable: true)
    .SetColumnFor(e => e.Gender, length: 1, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    ;

var company = db
    .DefineEntity(new
    {
        Id = 0,
        Name = ""
    })
    .SetNaming("Company")
    .SetColumnFor(e => e.Name, length: 100)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    ;

var customer = db
    .DefineEntity(new
    {
        Id = Guid.Empty,
        PersonId = 0,
        CompanyId = 0,
        CountryId = (short)0,
        AddressLine1 = "",
        AddressLine2 = "",
        Phone = "",
        Email = "",
        AlienGuid = Guid.Empty
    })
    .SetNaming("Customer")
    .SetColumnFor(e => e.PersonId, nullable: true)
    .SetColumnFor(e => e.CompanyId, nullable: true)
    .SetColumnFor(e => e.CountryId, nullable: true)
    .SetColumnFor(e => e.AddressLine1, length: 100, nullable: true)
    .SetColumnFor(e => e.AddressLine2, length: 100, nullable: true)
    .SetColumnFor(e => e.Email, length: 100, nullable: true)
    .SetColumnFor(e => e.Phone, length: 25, nullable: true)
    .SetColumnFor(e => e.AlienGuid, nullable: true)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.PersonId, person.Table)
    .AddForeignKey(e => e.CompanyId, company.Table)
    .AddForeignKey(e => e.CountryId, country.Table)
    ;

var card = db
    .DefineEntity(new
    {
        Id = Guid.Empty,
        CardTypeId = (short)0,
        IssuingNetwork = "",
        CardholderName = "",
        CardNumber = "",
        ExpirationDate = "",
        Cvv = ""
    })
    .SetNaming("Card")
    .SetColumnFor(e => e.IssuingNetwork, length: 25)
    .SetColumnFor(e => e.CardholderName, length: 100, nullable: true)
    .SetColumnFor(e => e.CardNumber, length: 20)
    .SetColumnFor(e => e.ExpirationDate, length: 6)
    .SetColumnFor(e => e.Cvv, length: 4)
    .SetPrimaryKey(e => e.Id)
    ;

var clientApplication = db
    .DefineEntity(new
    {
        Id = Guid.NewGuid(),
        Name = "",
        Url = ""
    })
    .SetNaming("ClientApplication")
    .SetColumnFor(e => e.Name, length: 100)
    .SetColumnFor(e => e.Url, length: 200, nullable: true)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Name)
    ;

var currency = db
    .DefineEntity(new
    {
        Id = (short)0,
        Name = "",
        Code = "",
        Rate = 0m
    })
    .SetNaming("Currency")
    .SetColumnFor(e => e.Name, length: 50)
    .SetColumnFor(e => e.Code, length: 5)
    .SetColumnFor(e => e.Rate, prec: 18, scale: 4)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    ;

var paymentTxn = db
    .DefineEntity(new
    {
        Id = (long)0,
        Guid = Guid.Empty,
        ClientFullClassName = "",
        PaymentTransactionStatusId = (short)0,
        ClientApplicationId = Guid.Empty,
        CustomerId = Guid.Empty,
        StoreId = 0,
        CardId = Guid.Empty,
        Amount = 0m,
        CurrencyId = (short)0,
        CurrencyRate = 0m,
        PaymentTransactionDateTime = DateTime.Now,
        Notes = ""
    })
    .SetNaming("PaymentTransaction")
    .SetColumnFor(e => e.ClientFullClassName, length: 200)
    .SetColumnFor(e => e.Amount, prec: 12, scale: 4)
    .SetColumnFor(e => e.CurrencyRate, prec: 18, scale: 4)
    .SetColumnFor(e => e.PaymentTransactionDateTime, nullable: true)
    .SetColumnFor(e => e.Notes, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddUnique(e => e.Guid)
    .AddForeignKey(e => e.ClientApplicationId, clientApplication.Table)
    .AddForeignKey(e => e.CustomerId, customer.Table)
    .AddForeignKey(e => e.CardId, card.Table)
    .AddForeignKey(e => e.CurrencyId, currency.Table)
    ;

var paymentTxnLog = db
    .DefineEntity(new
    {
        Id = (long)0,
        PaymentTransactionId = (long)0,
        PaymentTransactionStatusId = (short)0,
        LogType = "",
        ContentType = "",
        Content = "",
        Notes = ""
    })
    .SetNaming("PaymentTransactionLog")
    .SetColumnFor(e => e.LogType, length: 25, nullable: true)
    .SetColumnFor(e => e.ContentType, length: 100, nullable: true)
    .SetColumnFor(e => e.Notes, nullable: true)
    .SetIdentity(e => e.Id)
    .SetPrimaryKey(e => e.Id)
    .AddForeignKey(e => e.PaymentTransactionId, paymentTxn.Table)
    ;

dynamic importBag = new ExpandoObject();

importBag.ExtendedProperties = new List<ExtendedProperty>();

db.AddColumnForTables(new Column { Name = "Active", Type = "bit", ImportBag = importBag });
db.AddColumnForTables(new Column { Name = "CreationUser", Type = "nvarchar", Length = 50, ImportBag = importBag });
db.AddColumnForTables(new Column { Name = "CreationDateTime", Type = "datetime", ImportBag = importBag });
db.AddColumnForTables(new Column { Name = "LastUpdateUser", Type = "nvarchar", Length = 50, Nullable = true, ImportBag = importBag });
db.AddColumnForTables(new Column { Name = "LastUpdateDateTime", Type = "datetime", Nullable = true, ImportBag = importBag });
db.AddColumnForTables(new Column { Name = "Version", Type = "rowversion", Nullable = true, ImportBag = importBag });

SqlServerDatabaseScriptCodeBuilder.CreateScript(db, @"C:\Temp\Databases", true, true);

// Create instance of Entity Framework Core project
var project = EntityFrameworkCoreProject
    .CreateForV3x("RothschildHouse.Domain.Core", db, @"C:\Temp\RothschildHouse.Domain");

// Apply settings for Entity Framework Core project
project.GlobalSelection(settings =>
{
    settings.ForceOverwrite = true;
    settings.DeclareNavigationProperties = true;
    settings.DeclareNavigationPropertiesAsVirtual = true;
    settings.AddConfigurationForForeignKeysInFluentAPI = true;
});

// Build features for project, group all entities by schema into a feature
project.BuildFeatures();

// Scaffolding =^^=
project
    .ScaffoldDomain()
    ;
