namespace Store.Core.BusinessLayer.Responses
{
    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        int PageCount { get; }
    }
}
