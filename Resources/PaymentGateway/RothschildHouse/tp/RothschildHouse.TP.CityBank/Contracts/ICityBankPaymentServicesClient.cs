using RothschildHouse.TP.CityBank.DataContracts;

namespace RothschildHouse.TP.CityBank.Contracts
{
    public interface ICityBankPaymentServicesClient
    {
        Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
    }
}
