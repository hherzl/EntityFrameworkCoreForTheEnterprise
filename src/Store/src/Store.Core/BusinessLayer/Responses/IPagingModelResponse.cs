using System;

namespace Store.Core.BusinessLayer.Responses
{
    public interface IPagingModelResponse<TModel> : IListModelResponse<TModel>
    {
        Int32 ItemCount { get; set; }

        Int32 PageCount { get; }
    }
}
