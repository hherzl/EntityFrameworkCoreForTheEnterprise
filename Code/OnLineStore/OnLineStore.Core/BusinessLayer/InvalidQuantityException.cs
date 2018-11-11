namespace OnLineStore.Core.BusinessLayer
{
    public class InvalidQuantityException : OnLineStoreException
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
