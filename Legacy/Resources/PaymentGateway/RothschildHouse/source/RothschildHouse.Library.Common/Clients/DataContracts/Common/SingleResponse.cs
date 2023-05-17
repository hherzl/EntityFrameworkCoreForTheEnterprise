namespace RothschildHouse.Library.Common.Clients.DataContracts.Common
{
    public record SingleResponse<TModel> : Response where TModel : class, new()
    {
        public static SingleResponse<TModel> Empty =>
            new();

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
