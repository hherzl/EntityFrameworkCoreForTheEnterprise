namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record GetCustomersRequest
    {
        public GetCustomersRequest()
        {
            PageSize = 25;
            PageNumber = 1;
        }

        public GetCustomersRequest(string name, short? countryId, string phone, string email)
        {
            PageSize = 25;
            PageNumber = 1;
            Name = name;
            CountryID = countryId;
            Phone = phone;
            Email = email;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string Name { get; set; }
        public short? CountryID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
