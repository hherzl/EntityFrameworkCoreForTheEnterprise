namespace OnlineStore.Core.BusinessLayer
{
    public class DuplicatedProductNameException : OnlineStoreException
    {
        public DuplicatedProductNameException()
            : base()
        {
        }

        public DuplicatedProductNameException(string message)
            : base(message)
        {
        }
    }
}
