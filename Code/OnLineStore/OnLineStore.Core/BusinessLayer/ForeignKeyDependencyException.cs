namespace OnLineStore.Core.BusinessLayer
{
    public class ForeignKeyDependencyException : OnLineStoreException
    {
        public ForeignKeyDependencyException()
        {
        }

        public ForeignKeyDependencyException(string  message)
            : base(message)
        {
        }
    }
}
