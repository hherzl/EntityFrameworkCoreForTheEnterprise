namespace RothschildHouse.Application.Core.Common.Contracts
{
    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
