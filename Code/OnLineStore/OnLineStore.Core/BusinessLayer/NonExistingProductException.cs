namespace OnLineStore.Core.BusinessLayer
{
    public class NonExistingProductException : StoreException
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
