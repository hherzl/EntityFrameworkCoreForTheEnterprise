using RothschildHouse.Requests;

namespace RothschildHouse.Domain
{
#pragma warning disable CS1591
    public static class CreditCardExtensions
    {
        public static bool IsValid(this CreditCard creditCard, PostPaymentRequest request)
        {
            if (creditCard.IssuingNetwork != request.IssuingNetwork)
                return false;

            if (creditCard.CardNumber != request.CardNumber)
                return false;

            if (!creditCard.ExpirationDate.HasValue && !request.ExpirationDate.HasValue)
                return false;

            if (creditCard.ExpirationDate.Value.Year != request.ExpirationDate.Value.Year)
                return false;

            if (creditCard.ExpirationDate.Value.Month != request.ExpirationDate.Value.Month)
                return false;

            if (creditCard.Cvv != request.Cvv)
                return false;

            return true;
        }

        public static bool HasFounds(this CreditCard creditCard, PostPaymentRequest request)
        {
            if (creditCard.AvailableFounds <= request.Amount)
                return false;

            return true;
        }
    }
#pragma warning restore CS1591
}
