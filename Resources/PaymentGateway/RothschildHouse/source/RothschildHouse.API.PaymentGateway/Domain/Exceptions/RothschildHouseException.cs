namespace RothschildHouse.API.PaymentGateway.Domain.Exceptions
{
#pragma warning disable CS1591
    public class RothschildHouseException : Exception
    {
        public RothschildHouseException()
            : base()
        {
        }

        public RothschildHouseException(string message)
            : base(message)
        {
        }

        public RothschildHouseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
