using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record ProcessPaymentTransactionResponse : CreatedResponse<long?>
    {
        public ProcessPaymentTransactionResponse()
        {
        }

        public ProcessPaymentTransactionResponse(bool successed, string client, decimal? orderTotal, string currency)
        {
            Successed = successed;
            Client = client;
            OrderTotal = orderTotal;
            Currency = currency;
        }

        public bool Successed { get; set; }
        public string Client { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
    }
}
