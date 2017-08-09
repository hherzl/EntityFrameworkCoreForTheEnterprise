using System;

namespace Store.Core.BusinessLayer.Responses
{
    public interface IPagingResponse<TModel> : IListResponse<TModel>
    {
        Int32 ItemsCount { get; set; }

        Int32 PageCount { get; }
    }
}
