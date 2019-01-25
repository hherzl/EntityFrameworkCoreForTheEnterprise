using System.Net;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.BusinessLayer.Responses;

namespace OnlineStore.WebAPI.Responses
{
#pragma warning disable CS1591
    public static class Extensions
    {
        public static IActionResult ToHttpResponse<TModel>(this IListResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
                status = HttpStatusCode.InternalServerError;
            else if (response.Model == null)
                status = HttpStatusCode.NoContent;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
                status = HttpStatusCode.InternalServerError;
            else if (response.Model == null)
                status = HttpStatusCode.NotFound;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse(this IResponse response)
        {
            var status = response.DidError ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }
#pragma warning restore CS1591
}
