using System;

namespace OnlineStore.Core.BusinessLayer
{
    public class OnlineStoreException : Exception
    {
        public OnlineStoreException()
            : base()
        {
        }

        public OnlineStoreException(string message)
            : base(message)
        {
        }
    }
}
