namespace RothschildHouse.Clients.CityBank.Payloads.Avs.Mocks;

internal static class AvsMocks
{
    public static CityBankAvsPayload Unknown(string type, decimal? amount, string cardNumber, string expiry, string cardCode)
        => new()
        {
            Type = type,
            Amount = amount,
            PaymentMethodPayload = new()
            {
                Card = new()
                {
                    CardNumber = cardNumber,
                    Expiry = expiry,
                    CardCode = cardCode
                }
            }
        };

    public static CityBankAvsPayload NoMatch(string type, decimal? amount, string cardNumber, string expiry, string cardCode, string streetAddress, string postalCode)
        => new()
        {
            Type = type,
            Amount = amount,
            PaymentMethodPayload = new()
            {
                Card = new()
                {
                    CardNumber = cardNumber,
                    Expiry = expiry,
                    CardCode = cardCode
                },
                BillingAddress = new()
                {
                    StreetAddress1 = streetAddress,
                    PostalCode = postalCode
                }
            }
        };

    public static CityBankAvsPayload Zip(string type, decimal? amount, string cardNumber, string expiry, string cardCode, string streetAddress, string postalCode)
        => new()
        {
            Type = type,
            Amount = amount,
            PaymentMethodPayload = new()
            {
                Card = new()
                {
                    CardNumber = cardNumber,
                    Expiry = expiry,
                    CardCode = cardCode
                },
                BillingAddress = new()
                {
                    StreetAddress1 = streetAddress,
                    PostalCode = postalCode
                }
            }
        };

    public static CityBankAvsPayload Street(string type, decimal? amount, string cardNumber, string expiry, string cardCode, string streetAddress, string postalCode)
        => new()
        {
            Type = type,
            Amount = amount,
            PaymentMethodPayload = new()
            {
                Card = new()
                {
                    CardNumber = cardNumber,
                    Expiry = expiry,
                    CardCode = cardCode
                },
                BillingAddress = new()
                {
                    StreetAddress1 = streetAddress,
                    PostalCode = postalCode
                }
            }
        };

    public static CityBankAvsPayload StreetAndZip(string type, ProcessPaymentRequest request)
        => new()
        {
            Type = type,
            Amount = request.OrderTotal,
            PaymentMethodPayload = new()
            {
                Card = new()
                {
                    CardNumber = request.CardNumber,
                    Expiry = request.ExpirationDate,
                    CardCode = request.Cvv
                },
                BillingAddress = new()
                {
                    StreetAddress1 = request.Address,
                    PostalCode = request.PostalCode
                }
            }
        };
}
