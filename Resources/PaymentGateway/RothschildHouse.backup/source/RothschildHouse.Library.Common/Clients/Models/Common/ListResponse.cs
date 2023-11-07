namespace RothschildHouse.Library.Common.Clients.Models.Common
{
    public record ListResponse<TModel> : Response where TModel : class, new()
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
