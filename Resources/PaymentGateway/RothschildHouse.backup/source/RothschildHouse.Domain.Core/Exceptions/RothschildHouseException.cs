namespace RothschildHouse.Domain.Core.Exceptions
{
    public class RothschildHouseException : Exception
    {
        public RothschildHouseException()
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
