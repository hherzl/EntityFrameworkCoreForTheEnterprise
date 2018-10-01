namespace Store.Core.BusinessLayer
{
    public class ForeignKeyDependencyException : StoreException
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
