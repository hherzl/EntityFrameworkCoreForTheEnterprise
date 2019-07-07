using System;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Core.BusinessLayer.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, ILogger logger, string actionName, Exception ex)
        {
            // todo: Save error in log file
            // reference: https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/

            response.DidError = true;

            if (ex is OnlineStoreException cast)
            {
                logger?.LogError("There was an error on '{0}': {1}", actionName, ex);

                response.ErrorMessage = ex.Message;
            }
            else
            {
                logger?.LogCritical("There was a critical error on '{0}': {1}", actionName, ex);

                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
        }
    }
}
