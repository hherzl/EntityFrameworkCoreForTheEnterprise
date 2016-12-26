using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Store.API.ViewModels;

namespace Store.API.Extensions
{
    public static class ViewModelsExtensions
    {
        public static IActionResult ToHttpResponse(this CreateOrderViewModel viewModel)
        {
            return new ObjectResult(viewModel)
            {
                StatusCode = (Int32)HttpStatusCode.OK
            };
        }
    }
}
