﻿namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public record CardItemModel
    {
        public Guid? Id { get; set; }
        public short? CardTypeId { get; set; }
        public string CardType { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
    }
}
