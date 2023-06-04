namespace RothschildHouse.Library.Common.Clients.Models.Common
{
    public record PagedResponse<TModel> : ListResponse<TModel> where TModel : class, new()
    {
        public PagedResponse()
        {
        }

        public PagedResponse(IEnumerable<TModel> model)
            : base(model)
        {
        }

        public PagedResponse(IEnumerable<TModel> model, int pageSize, int pageNumber, int itemsCount)
            : base(model)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            ItemsCount = itemsCount;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ItemsCount { get; set; }

        public int PageCount
            => ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);
    }
}
