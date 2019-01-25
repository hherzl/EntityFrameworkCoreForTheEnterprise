namespace OnlineStore.Core.BusinessLayer.Responses
{
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
