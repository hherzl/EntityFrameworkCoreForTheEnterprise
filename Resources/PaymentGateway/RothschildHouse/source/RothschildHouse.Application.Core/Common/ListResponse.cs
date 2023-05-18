using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Common
{
    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public IEnumerable<TModel> Model { get; set; }
    }
}
