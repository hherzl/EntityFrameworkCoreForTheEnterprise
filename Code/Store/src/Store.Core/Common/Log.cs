using System;

namespace Store.Core.Common
{
    public class Log : ILog
    {
        public void Write(String message)
        {
            // todo: add logic to log message

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            Console.WriteLine(message);
        }
    }
}
