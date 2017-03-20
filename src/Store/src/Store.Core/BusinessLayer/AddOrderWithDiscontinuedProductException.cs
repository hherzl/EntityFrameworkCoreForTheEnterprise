using System;

namespace Store.Core.BusinessLayer
{
    public class AddOrderWithDiscontinuedProductException : StoreException
    {
        public AddOrderWithDiscontinuedProductException()
            : base()
        {
        }

        public AddOrderWithDiscontinuedProductException(String message)
            : base(message)
        {
        }
    }
}
