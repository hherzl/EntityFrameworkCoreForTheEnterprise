namespace OnLineStore.Core.BusinessLayer
{
    public class AddOrderWithDiscontinuedProductException : OnLineStoreException
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
