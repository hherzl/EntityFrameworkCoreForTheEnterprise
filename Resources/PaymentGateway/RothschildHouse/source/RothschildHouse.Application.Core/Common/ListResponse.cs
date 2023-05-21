using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Common
{
    public record ListResponse<TModel> : Response, IListResponse<TModel>
    {
        public ListResponse()
        {
        }

        public ListResponse(IEnumerable<TModel> model)
        {
            Model = new List<TModel>(model);
        }

        public IEnumerable<TModel> Model { get; set; }
    }
}
