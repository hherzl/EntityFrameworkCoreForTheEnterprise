namespace OnLineStore.Core.BusinessLayer
{
    public class DuplicatedProductNameException : StoreException
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
