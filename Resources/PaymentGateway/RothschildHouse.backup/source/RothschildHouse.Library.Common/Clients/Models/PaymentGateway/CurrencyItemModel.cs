﻿namespace RothschildHouse.Library.Common.Clients.Models.PaymentGateway
{
    public record CurrencyItemModel
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
    }
}
