namespace RothschildHouse.Clients.CityBank;

public interface ICityBankPaymentServicesClient
{
    Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
}
