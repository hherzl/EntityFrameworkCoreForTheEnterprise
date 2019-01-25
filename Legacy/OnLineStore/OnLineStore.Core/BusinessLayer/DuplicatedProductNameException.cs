namespace OnLineStore.Core.BusinessLayer
{
    public class DuplicatedProductNameException : OnLineStoreException
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
