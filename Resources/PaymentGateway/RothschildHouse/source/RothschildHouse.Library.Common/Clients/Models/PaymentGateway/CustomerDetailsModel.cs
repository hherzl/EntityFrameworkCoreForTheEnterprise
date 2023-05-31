namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record CustomerDetailsModel
    {
        public Guid? Id { get; set; }
        public int? PersonId { get; set; }
        public string Person { get; set; }
        public int? CompanyId { get; set; }
        public string Company { get; set; }
        public short? CountryId { get; set; }
        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? AlienGuid { get; set; }

        public List<PaymentTransactionItemModel> PaymentTransactions { get; set; }
    }
}
