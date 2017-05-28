using System;
using Microsoft.Extensions.Logging;
using Store.Core.BusinessLayer.Responses;

namespace Store.Core.BusinessLayer
{
    public static class ResponseExtensions
    {
        public static void SetError<TModel>(this IListModelResponse<TModel> response, Exception ex, ILogger logger)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                logger?.LogCritical(ex.ToString());
            }
            else
            {
                response.ErrorMessage = ex.Message;

                logger?.LogError(ex.Message);
            }
        }

        public static void SetError<TModel>(this ISingleModelResponse<TModel> response, Exception ex, ILogger logger)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                logger?.LogCritical(ex.ToString());
            }
            else
            {
                response.ErrorMessage = ex.Message;

                logger?.LogError(ex.Message);
            }
        }
    }
}
