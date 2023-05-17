namespace OnlineStore.Core.Business
{
    public class NonExistingProductException : OnlineStoreException
    {
        public NonExistingProductException()
            : base()
        {
        }

        public NonExistingProductException(string  message)
            : base(message)
        {
        }
    }
}
