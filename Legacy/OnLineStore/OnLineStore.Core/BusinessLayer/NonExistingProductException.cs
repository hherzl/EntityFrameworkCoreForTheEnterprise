namespace OnLineStore.Core.BusinessLayer
{
    public class NonExistingProductException : OnLineStoreException
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
