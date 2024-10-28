namespace OnlineStore.Core.Business
{
    public class AddOrderWithDiscontinuedProductException : OnlineStoreException
    {
        public AddOrderWithDiscontinuedProductException()
            : base()
        {
        }

        public AddOrderWithDiscontinuedProductException(string  message)
            : base(message)
        {
        }
    }
}
