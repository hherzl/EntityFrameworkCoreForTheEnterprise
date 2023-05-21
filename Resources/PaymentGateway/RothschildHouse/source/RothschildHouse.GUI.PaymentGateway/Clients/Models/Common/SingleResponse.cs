namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.Common
{
    public record SingleResponse<TModel> where TModel : class, new()
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
