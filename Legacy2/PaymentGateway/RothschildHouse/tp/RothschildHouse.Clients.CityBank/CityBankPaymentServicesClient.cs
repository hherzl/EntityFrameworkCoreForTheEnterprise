using System.Text.Json;
using RothschildHouse.Clients.CityBank.Payloads.Avs.Mocks;
using RothschildHouse.Clients.CityBank.Payloads.Cvv2.Mocks;
using RothschildHouse.Clients.CityBank.Payloads.ProcessPayment.Mocks;

namespace RothschildHouse.Clients.CityBank;

public class CityBankPaymentServicesClient : ICityBankPaymentServicesClient
{
    private readonly Database _database;

    public CityBankPaymentServicesClient()
    {
        _database = new();
        _database.Init();
    }

    public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
    {
        var card = _database.Cards.FirstOrDefault(item => item.CardNumber == request.CardNumber);

        if (card == null)
            return await Task.FromResult(ProcessPaymentResponse.Failed);

        await Task.Delay(500);

        var response = new ProcessPaymentResponse
        {
            Successed = true,
            Guid = Guid.NewGuid(),
            AvsPayload = AvsMocks.StreetAndZip("payment", request),
            Cvv2Payload = Cvv2Mocks.Match(),
            Authorization = ProcessPaymentMocks.MockAuthorization(),
            Capture = ProcessPaymentMocks.MockCapture(),
            SubscriptionTransactionId = ""
        };

        return await Task.FromResult(response);
    }
}
