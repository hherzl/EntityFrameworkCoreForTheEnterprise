namespace RothschildHouse.Library.Common.Clients.DataContracts.Common
{
    public record ListResponse<TModel> : Response where TModel : class
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
