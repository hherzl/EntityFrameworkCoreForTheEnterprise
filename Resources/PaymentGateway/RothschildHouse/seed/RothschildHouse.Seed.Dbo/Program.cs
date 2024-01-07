using RothschildHouse.Seed.Dbo;
using RothschildHouse.Seed.Dbo.Seeds;

using var ctx = DbContextHelper.GetRothschildHouseDbContext();

Console.WriteLine($"Creating countries...");

foreach (var item in Countries.Items)
{
    ctx.Country.Add(item);

    await ctx.SaveChangesAsync();
}

Console.WriteLine($"Creating currencies...");

foreach (var item in Currencies.Items)
{
    ctx.Currency.Add(item);

    await ctx.SaveChangesAsync();
}

Console.WriteLine($" The currencies were created successfully");

Console.WriteLine($"Creating client applications...");

foreach (var item in ClientApplications.Items)
{
    ctx.ClientApplication.Add(item);

    await ctx.SaveChangesAsync();
}

Console.WriteLine($" The client applications were created successfully");

Console.WriteLine($"Creating descriptions for enums...");

foreach (var item in VCardTypes.Items)
{
    ctx.EnumDescription.Add(item);

    await ctx.SaveChangesAsync();
}

foreach (var item in VCustomerTypes.Items)
{
    ctx.EnumDescription.Add(item);

    await ctx.SaveChangesAsync();
}

foreach (var item in VTransactionTypes.Items)
{
    ctx.EnumDescription.Add(item);

    await ctx.SaveChangesAsync();
}

foreach (var item in VTransactionStatuses.Items)
{
    ctx.EnumDescription.Add(item);

    await ctx.SaveChangesAsync();
}

Console.WriteLine($" The descriptions for enums were created successfully");

Console.WriteLine($"Creating person customers...");

foreach (var item in PersonCustomers.Items)
{
    ctx.Person.Add(item.Item1);

    await ctx.SaveChangesAsync();

    item.Item2.PersonId = item.Item1.Id;

    ctx.Customer.Add(item.Item2);

    await ctx.SaveChangesAsync();
}

Console.WriteLine($" The person customers were created successfully");
