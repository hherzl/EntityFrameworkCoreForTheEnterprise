namespace OnlineStore.Core.BusinessLayer
{
    public class ForeignKeyDependencyException : OnlineStoreException
    {
        public ForeignKeyDependencyException()
            : base()
        {
        }

        public ForeignKeyDependencyException(string  message)
            : base(message)
        {
        }
    }
}
