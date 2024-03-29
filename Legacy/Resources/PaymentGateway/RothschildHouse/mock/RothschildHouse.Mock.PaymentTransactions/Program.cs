﻿// discover endpoints from metadata
using IdentityModel.Client;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Mock.PaymentTransactions;

using var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:37200");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}
else
{
    Console.WriteLine(disco.TokenEndpoint);
}

//var token = await client.RequestTokenAsync(new TokenRequest
//{
//    ClientId = "",
//    ClientSecret = ""
//});

var rothschildHouseClient = new RothschildHouseClient();
var orderTotal = 9.99m;

while (true)
{
    var request = new ProcessPaymentTransactionCommand
    {
        ClientApplication = Guid.Parse("B74CB3C2-BB35-4436-BCFB-8769B521CA3D"),
        CustomerGuid = Guid.Parse("867D280C-8BFA-4ACB-B64E-76BAAD10B63D"),
        StoreId = 0,
        CardTypeId = 2000,
        IssuingNetwork = "VISA",
        CardholderName = "Juan Pérez",
        CardNumber = "4012888888881881",
        ExpirationDate = "0124",
        Cvv = "123",
        OrderGuid = Guid.NewGuid(),
        OrderTotal = orderTotal,
        Currency = "USD"
    };

    orderTotal += 1;

    Console.WriteLine($"{DateTime.Now}: Processing payment transaction: total: '{request.OrderTotal} {request.Currency}'...");

    var response = await rothschildHouseClient.ProcessPaymentTransactionAsync(request);

    Console.WriteLine($"{DateTime.Now}:  Transaction ID: '{response.Id}', Client: '{response.Client}', Amount: '{response.OrderTotal} {response.Currency}'");

    await Task.Delay(3000);
}
