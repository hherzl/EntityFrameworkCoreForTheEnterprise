namespace OnlineStore.Core.Business.Responses
{
    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        double PageCount { get; }
    }
}
