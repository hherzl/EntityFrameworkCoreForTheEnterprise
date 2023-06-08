using RothschildHouse.Seed.PaymentGateway.Helpers;
using RothschildHouse.Seed.PaymentGateway.Seeds;

const string CreationUser = "seed";

using var ctx = DbContextHelper.GetRothschildHouseDbContext();

Console.WriteLine($"Creating countries...");

foreach (var item in Countries.Items)
{
    item.Active = true;
    item.CreationUser = CreationUser;
    item.CreationDateTime = DateTime.Now;

    ctx.Country.Add(item);

    ctx.SaveChanges();
}

Console.WriteLine($"Creating currencies...");

foreach (var item in Currencies.Items)
{
    item.Active = true;
    item.CreationUser = CreationUser;
    item.CreationDateTime = DateTime.Now;

    ctx.Currency.Add(item);

    ctx.SaveChanges();
}

Console.WriteLine($" The currencies were created successfully");

Console.WriteLine($"Creating client applications...");

foreach (var item in ClientApplications.Items)
{
    item.Active = true;
    item.CreationUser = CreationUser;
    item.CreationDateTime = DateTime.Now;

    ctx.ClientApplication.Add(item);

    ctx.SaveChanges();
}

Console.WriteLine($" The client applications were created successfully");

Console.WriteLine($"Creating descriptions for enums...");

foreach (var item in VCardTypes.Items)
{
    item.Active = true;

    ctx.EnumDescription.Add(item);

    ctx.SaveChanges();
}

foreach (var item in VCustomerTypes.Items)
{
    item.Active = true;

    ctx.EnumDescription.Add(item);

    ctx.SaveChanges();
}

foreach (var item in VTransactionStatuses.Items)
{
    item.Active = true;

    ctx.EnumDescription.Add(item);

    ctx.SaveChanges();
}

Console.WriteLine($" The descriptions for enums were created successfully");

Console.WriteLine($"Creating person customers...");

foreach (var item in PersonCustomers.Items)
{
    item.Item1.Active = true;
    item.Item1.CreationUser = CreationUser;
    item.Item1.CreationDateTime = DateTime.Now;

    ctx.Person.Add(item.Item1);

    ctx.SaveChanges();

    item.Item2.PersonId = item.Item1.Id;
    item.Item2.Active = true;
    item.Item2.CreationUser = CreationUser;
    item.Item2.CreationDateTime = DateTime.Now;

    ctx.Customer.Add(item.Item2);

    ctx.SaveChanges();
}

Console.WriteLine($" The person customers were created successfully");
