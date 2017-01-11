using System;
using Store.Core.BusinessLayer.Responses;
using Store.Core.Common;

namespace Store.Core.BusinessLayer
{
    public static class ResponseExtensions
    {
        public static void SetError<TModel>(this IListModelResponse<TModel> response, Exception ex, ILog logger)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                logger.Write(ex.ToString());
            }
            else
            {
                response.ErrorMessage = ex.Message;
            }
        }

        public static void SetError<TModel>(this ISingleModelResponse<TModel> response, Exception ex, ILog logger)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                logger.Write(ex.ToString());
            }
            else
            {
                response.ErrorMessage = ex.Message;
            }
        }
    }
}
