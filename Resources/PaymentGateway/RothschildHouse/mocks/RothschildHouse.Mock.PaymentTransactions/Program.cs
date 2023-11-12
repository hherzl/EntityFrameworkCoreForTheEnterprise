using System.Diagnostics;
using RothschildHouse.Mock.PaymentTransactions;

var mocksRandom = new Random();

var mocks = new
{
    ClientApplications = Mocks.ClientApplications.Items.ToList(),
    Customers = Mocks.Customers.Items.ToList(),
    Cards = Mocks.Cards.Items.ToList(),
    Currencies = Mocks.Currencies.Items.ToList()
};

var now = DateTime.Now;

var settings = new MockSettings();

if (args.Length > 0)
{
    foreach (var arg in args)
    {
        if (arg.StartsWith("--start-date"))
        {
            var value = arg.Split('=')[1];
            if (value == "now")
            {
                settings.StartDate = new DateTime(now.Year, now.Month, now.Day);
                settings.EndDate = new DateTime(now.Year, now.Month, now.Day).AddDays(1);
            }
        }
        else if (arg.StartsWith("--transactions-per-day"))
        {
            var value = arg.Split('=')[1];
            settings.TransactionsPerDay = Convert.ToInt32(value);
        }
    }
}

Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine($"Settings: '{settings}'");
Console.WriteLine($"Starting at '{DateTime.Now}'");
Console.WriteLine();

var duration = new Stopwatch();

duration.Start();

while (settings.StartDate < settings.EndDate)
{
    var transactionsPerDay = mocksRandom.Next(settings.TransactionsPerDay);

    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine($"Mocking '{transactionsPerDay}' transactions for {settings.StartDate}");

    Console.ForegroundColor = ConsoleColor.DarkBlue;

    var rothschildHouseClient = new RothschildHouseClient();

    for (var i = 1; i <= transactionsPerDay; i++)
    {
        var clientApplicationIndex = mocksRandom.Next(mocks.ClientApplications.Count);
        var customerIndex = mocksRandom.Next(mocks.Customers.Count);
        var cardIndex = mocksRandom.Next(mocks.Cards.Count);
        var currencyIndex = mocksRandom.Next(mocks.Currencies.Count);
        var card = mocks.Cards[cardIndex];
        var orderTotal = (decimal)mocksRandom.Next(1, 100);

        var request = new ProcessTransactionRequest
        {
            ClientApplication = mocks.ClientApplications[clientApplicationIndex],
            CustomerGuid = mocks.Customers[customerIndex],
            StoreId = 0,
            CardTypeId = card.Item1,
            IssuingNetwork = card.Item2,
            CardholderName = card.Item3,
            CardNumber = card.Item4,
            ExpirationDate = card.Item5,
            Cvv = card.Item6,
            OrderGuid = Guid.NewGuid(),
            OrderTotal = orderTotal,
            Currency = mocks.Currencies[currencyIndex],
            TransactionDateTime = settings.StartDate
        };

        Console.WriteLine($" {DateTime.Now}: Processing transaction: cardholder name: '{request.CardholderName}', total: '{request.OrderTotal} {request.Currency}'...");

        var response = await rothschildHouseClient.ProcessTransactionAsync(request);

        Console.WriteLine($"  {DateTime.Now}:  Txn Id: '{response.Id}', client: '{response.Client}', total: '{response.OrderTotal} {response.Currency}'");
    }

    settings.StartDate = settings.StartDate.AddDays(1);

    Console.WriteLine();

    await Task.Delay(100);
}

Console.ForegroundColor = ConsoleColor.Green;

Console.WriteLine($"Ending at '{DateTime.Now}'");

duration.Stop();

Console.WriteLine($"Total time: '{duration.Elapsed}'");

Console.ForegroundColor = ConsoleColor.White;
