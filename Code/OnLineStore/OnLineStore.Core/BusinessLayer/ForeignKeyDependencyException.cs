namespace OnLineStore.Core.BusinessLayer
{
    public class ForeignKeyDependencyException : OnLineStoreException
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
