namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.Common
{
    public record ListResponse<TModel>
    {
        public List<TModel> Model { get; set; }
    }
}
