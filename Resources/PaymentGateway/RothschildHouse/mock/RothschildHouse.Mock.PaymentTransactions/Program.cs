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

var startDate = new DateTime(now.Year, 1, 1);
var endDate = new DateTime(now.Year, now.Month, now.Day);

if (args.Length > 0)
{
    if (args[0] == "--start-date=now")
    {
        startDate = new DateTime(now.Year, now.Month, now.Day);
        endDate = new DateTime(now.Year, now.Month, now.Day).AddDays(1);
    }
}

while (startDate < endDate)
{
    var transactionsPerDay = mocksRandom.Next(25);

    Console.WriteLine($"Mocking '{transactionsPerDay}' transactions for {startDate}");

    var rothschildHouseClient = new RothschildHouseClient();

    for (var i = 1; i <= transactionsPerDay; i++)
    {
        var clientApplicationIndex = mocksRandom.Next(mocks.ClientApplications.Count);
        var customerIndex = mocksRandom.Next(mocks.Customers.Count);
        var cardIndex = mocksRandom.Next(mocks.Cards.Count);
        var currencyIndex = mocksRandom.Next(mocks.Currencies.Count);
        var card = mocks.Cards[cardIndex];
        var orderTotal = (decimal)mocksRandom.Next(1, 50);

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
            TransactionDateTime = startDate
        };

        Console.WriteLine($"{DateTime.Now}: Processing transaction: cardholder name: '{request.CardholderName}', total: '{request.OrderTotal} {request.Currency}'...");

        var response = await rothschildHouseClient.ProcessTransactionAsync(request);

        Console.WriteLine($"{DateTime.Now}:  Transaction Id: '{response.Id}', Client: '{response.Client}', Amount: '{response.OrderTotal} {response.Currency}'");
    }

    startDate = startDate.AddDays(1);

    Console.WriteLine();

    await Task.Delay(100);
}
