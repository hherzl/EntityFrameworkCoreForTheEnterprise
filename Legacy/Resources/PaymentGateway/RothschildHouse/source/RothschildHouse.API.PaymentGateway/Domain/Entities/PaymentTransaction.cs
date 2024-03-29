﻿using System.Collections.ObjectModel;
using RothschildHouse.API.PaymentGateway.Domain.Common;

namespace RothschildHouse.API.PaymentGateway.Domain.Entities
{
#pragma warning disable CS1591
    public class PaymentTransaction : AuditableEntity
    {
        public PaymentTransaction()
        {
        }

        public long? Id { get; set; }
        public Guid? Guid { get; set; }
        public string ClientFullClassName { get; set; }
        public short? PaymentTransactionStatusId { get; set; }
        public Guid? ClientApplicationId { get; set; }
        public Guid? CustomerId { get; set; }
        public int? StoreId { get; set; }
        public Guid? CardId { get; set; }
        public decimal? Amount { get; set; }
        public short? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? PaymentTransactionDateTime { get; set; }
        public string Notes { get; set; }

        public virtual ClientApplication ClientApplicationFk { get; set; }
        public virtual Customer CustomerFk { get; set; }
        public virtual Card CardFk { get; set; }
        public virtual Currency CurrencyFk { get; set; }
        public virtual Collection<PaymentTransactionLog> PaymentTransactionLogList { get; set; }
    }
}
