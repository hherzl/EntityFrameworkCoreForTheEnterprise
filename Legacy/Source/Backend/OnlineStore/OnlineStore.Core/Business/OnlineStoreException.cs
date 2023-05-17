using System;

namespace OnlineStore.Core.Business
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
