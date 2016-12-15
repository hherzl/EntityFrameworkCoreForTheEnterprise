using System;

namespace Store.Core.BusinessLayer
{
    public class DuplicatedProductNameException : StoreException
    {
        public DuplicatedProductNameException()
            : base()
        {
        }

        public DuplicatedProductNameException(String message)
            : base(message)
        {
        }
    }
}
