using System;
using System.Collections.Generic;

namespace Store.Core.BusinessLayer.Responses
{
    public interface IListModelResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
