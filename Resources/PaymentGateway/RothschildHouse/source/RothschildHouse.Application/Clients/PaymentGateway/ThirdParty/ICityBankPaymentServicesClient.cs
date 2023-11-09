namespace RothschildHouse.Application.Clients.PaymentGateway.ThirdParty;

public interface ICityBankPaymentServicesClient
{
    Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
}
