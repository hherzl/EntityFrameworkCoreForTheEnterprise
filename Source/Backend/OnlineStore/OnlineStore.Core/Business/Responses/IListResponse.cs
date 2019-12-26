using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Business.Responses
{
    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
