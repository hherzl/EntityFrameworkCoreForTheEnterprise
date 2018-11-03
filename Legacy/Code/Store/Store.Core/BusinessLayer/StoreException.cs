using System;

namespace Store.Core.BusinessLayer
{
    public class StoreException : Exception
    {
        public StoreException()
            : base()
        {
        }

        public StoreException(string  message)
            : base(message)
        {
        }

        public StoreException(string  message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
