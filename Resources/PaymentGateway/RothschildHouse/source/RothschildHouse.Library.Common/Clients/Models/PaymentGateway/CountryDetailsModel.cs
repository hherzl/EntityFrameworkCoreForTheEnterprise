﻿namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record CountryDetailsModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
    }
}
