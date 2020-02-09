namespace OnlineStore.Core.Business
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
