using RothschildHouse._3P.CityBank.Models;

namespace RothschildHouse._3P.CityBank.Contracts
{
    public interface ICityBankPaymentServicesClient
    {
        Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
    }
}
