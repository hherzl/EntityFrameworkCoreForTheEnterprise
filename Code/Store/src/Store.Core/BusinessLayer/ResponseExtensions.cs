using System;
using Store.Core.BusinessLayer.Responses;

namespace Store.Core.BusinessLayer
{
    public static class ResponseExtensions
    {
        public static void SetError<TModel>(this IListModelResponse<TModel> response, Exception ex)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                response.ErrorMessage = ex.Message;
            }
            else
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
        }

        public static void SetError<TModel>(this ISingleModelResponse<TModel> response, Exception ex)
        {
            response.DidError = true;

            var cast = ex as StoreException;

            if (cast == null)
            {
                response.ErrorMessage = "There was an internal error, please contact to technical support.";
            }
            else
            {
                response.ErrorMessage = ex.Message;
            }
        }
    }
}
