using RothschildHouse.Mock.PaymentTransactions;

var mocksRandom = new Random();

var mocks = new
{
    ClientApplications = Mocks.ClientApplications.Items.ToList(),
    Customers = Mocks.Customers.Items.ToList(),
    Cards = Mocks.Cards.Items.ToList(),
    Currency = "USD"
};

while (true)
{
    var clientApplicationIndex = mocksRandom.Next(mocks.ClientApplications.Count);
    var customerIndex = mocksRandom.Next(mocks.Customers.Count);
    var cardIndex = mocksRandom.Next(mocks.Cards.Count);
    var card = mocks.Cards[cardIndex];
    var orderTotal = (decimal)mocksRandom.Next(1, 100);

    var request = new ProcessPaymentTransactionCommand
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
        Currency = mocks.Currency
    };

    var rothschildHouseClient = new RothschildHouseClient();

    Console.WriteLine($"{DateTime.Now}: Processing payment transaction: total: '{request.OrderTotal} {request.Currency}'...");

    var response = await rothschildHouseClient.ProcessPaymentTransactionAsync(request);

    Console.WriteLine($"{DateTime.Now}:  Transaction ID: '{response.Id}', Client: '{response.Client}', Amount: '{response.OrderTotal} {response.Currency}'");

    await Task.Delay(3000);
}
