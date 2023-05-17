using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using MediatR;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public class ProcessPaymentTransactionCommand : IRequest<ProcessPaymentTransactionResponse>, IValidatableObject
    {
        public ProcessPaymentTransactionCommand()
        {
        }

        public Guid? ClientApplication { get; set; }
        public Guid? CustomerGuid { get; set; }
        public int? StoreId { get; set; }

        public short? CardTypeId { get; set; }
        public string IssuingNetwork { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public Guid? OrderGuid { get; set; }
        public decimal? OrderTotal { get; set; }
        public string Currency { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public string Notes { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ClientApplication == null)
                yield return new ValidationResult("The client application is required", new string[] { nameof(ClientApplication) });

            if (!CustomerGuid.HasValue)
                yield return new ValidationResult("The customer guid is required", new string[] { nameof(CustomerGuid) });

            if (!StoreId.HasValue)
                yield return new ValidationResult("The store is required", new string[] { nameof(StoreId) });

            if (string.IsNullOrEmpty(IssuingNetwork))
                yield return new ValidationResult("The issuing network is required", new string[] { nameof(IssuingNetwork) });
            else if (IssuingNetwork.Length > 25)
                yield return new ValidationResult("The maximum length for issuing network is 25", new string[] { nameof(IssuingNetwork) });

            if (string.IsNullOrEmpty(CardholderName))
                yield return new ValidationResult("The cardholder name is required", new string[] { nameof(CardholderName) });
            else if (CardholderName.Length > 50)
                yield return new ValidationResult("The maximum length for cardholder name is 50", new string[] { nameof(CardholderName) });

            if (string.IsNullOrEmpty(CardNumber))
                yield return new ValidationResult("The card number is required", new string[] { nameof(CardNumber) });
            else if (CardNumber.Length > 20)
                yield return new ValidationResult("The maximum lenght for card number is 20", new string[] { nameof(CardNumber) });

            if (string.IsNullOrEmpty(ExpirationDate))
                yield return new ValidationResult("The expiration date is required", new string[] { nameof(ExpirationDate) });

            if (string.IsNullOrEmpty(Cvv))
                yield return new ValidationResult("The CVV is required", new string[] { nameof(Cvv) });
            else if (Cvv.Length > 4)
                yield return new ValidationResult("The maximum lenght for CVV is 4", new string[] { nameof(Cvv) });

            if (!OrderGuid.HasValue)
                yield return new ValidationResult("The order guid is required", new string[] { nameof(OrderGuid) });

            if (!OrderTotal.HasValue)
                yield return new ValidationResult("The order total is required", new string[] { nameof(OrderTotal) });

            if (OrderTotal <= 0)
                yield return new ValidationResult("The order total must be greater than zero", new string[] { nameof(OrderTotal) });

            if (string.IsNullOrEmpty(Currency))
                yield return new ValidationResult("The currency is required", new string[] { nameof(Currency) });
        }
    }
}
