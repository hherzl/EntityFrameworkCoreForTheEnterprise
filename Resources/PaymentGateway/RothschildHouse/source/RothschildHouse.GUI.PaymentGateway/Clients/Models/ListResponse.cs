namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record ListResponse<TModel>
    {
        public List<TModel> Model { get; set; }
    }
}
