using System;

namespace Store.Core.BusinessLayer
{
    public class NonExistingProductException : StoreException
    {
        public NonExistingProductException()
            : base()
        {
        }

        public NonExistingProductException(String message)
            : base(message)
        {
        }
    }
}
