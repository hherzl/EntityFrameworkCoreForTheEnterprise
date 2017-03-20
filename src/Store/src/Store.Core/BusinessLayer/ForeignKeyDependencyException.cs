using System;

namespace Store.Core.BusinessLayer
{
    public class ForeignKeyDependencyException : StoreException
    {
        public ForeignKeyDependencyException()
        {
        }

        public ForeignKeyDependencyException(String message)
            : base(message)
        {
        }
    }
}
