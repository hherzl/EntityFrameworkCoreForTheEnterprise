namespace RothschildHouse.Library.Common.Clients.Models.Common
{
    public record SingleResponse<TModel> : Response where TModel : class, new()
    {
        public SingleResponse()
        {
        }

        public SingleResponse(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; set; }
    }
}
