namespace OnlineStore.Core.Business
{
    public class InvalidQuantityException : OnlineStoreException
    {
        public InvalidQuantityException()
            : base()
        {
        }

        public InvalidQuantityException(string message)
            : base(message)
        {
        }
    }
}
