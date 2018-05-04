using System;
using System.Collections.Generic;

namespace Store.Core.BusinessLayer.Responses
{
    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
