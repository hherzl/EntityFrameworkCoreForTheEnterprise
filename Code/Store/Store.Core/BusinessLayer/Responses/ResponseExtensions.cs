using System;
using Microsoft.Extensions.Logging;

namespace Store.Core.BusinessLayer.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, Exception ex, ILogger logger)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                logger?.LogCritical(ex.ToString());
                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
            else
            {
                logger?.LogError(ex.Message);
                response.ErrorMessage = ex.Message;
            }
        }
    }
}
