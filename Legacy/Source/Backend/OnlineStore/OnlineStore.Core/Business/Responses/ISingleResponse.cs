namespace OnlineStore.Core.Business.Responses
{
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
