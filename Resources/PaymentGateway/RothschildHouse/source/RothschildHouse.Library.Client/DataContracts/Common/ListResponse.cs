using RothschildHouse.Library.Client.DataContracts.Common.Contracts;

namespace RothschildHouse.Library.Client.DataContracts.Common
{
    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public IEnumerable<TModel> Model { get; set; }
    }
}
