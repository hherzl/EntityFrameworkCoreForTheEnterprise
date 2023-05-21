namespace RothschildHouse.GUI.PaymentGateway.Clients.Models.Common
{
    public record ListResponse<TModel> : Response
    {
        public ListResponse()
        {
        }

        public ListResponse(IEnumerable<TModel> model)
        {
            Model = new List<TModel>(model);
        }

        public List<TModel> Model { get; set; }
    }
}
