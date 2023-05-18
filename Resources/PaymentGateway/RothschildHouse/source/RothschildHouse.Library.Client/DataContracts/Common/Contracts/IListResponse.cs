namespace RothschildHouse.Library.Client.DataContracts.Common.Contracts
{
    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
