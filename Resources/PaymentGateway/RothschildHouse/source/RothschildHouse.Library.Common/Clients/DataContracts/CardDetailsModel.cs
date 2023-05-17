namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record CardDetailsModel
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }
}
