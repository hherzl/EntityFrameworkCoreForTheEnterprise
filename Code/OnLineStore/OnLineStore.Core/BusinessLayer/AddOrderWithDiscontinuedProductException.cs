namespace OnLineStore.Core.BusinessLayer
{
    public class AddOrderWithDiscontinuedProductException : StoreException
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
