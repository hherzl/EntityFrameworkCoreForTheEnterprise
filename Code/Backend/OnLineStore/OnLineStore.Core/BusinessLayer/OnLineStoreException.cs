using System;

namespace OnLineStore.Core.BusinessLayer
{
    public class OnLineStoreException : Exception
    {
        public OnLineStoreException()
            : base()
        {
        }

        public OnLineStoreException(string message)
            : base(message)
        {
        }
    }
}
