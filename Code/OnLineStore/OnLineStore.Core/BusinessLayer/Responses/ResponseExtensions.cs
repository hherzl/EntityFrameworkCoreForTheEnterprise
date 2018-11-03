using System;
using Microsoft.Extensions.Logging;

namespace OnLineStore.Core.BusinessLayer.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, Exception ex, ILogger logger)
        {
            response.DidError = true;

            if (ex is StoreException cast)
            {
                logger?.LogError(ex.Message);

                response.ErrorMessage = ex.Message;
            }
            else
            {
                logger?.LogCritical(ex.ToString());

                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
        }
    }
}
