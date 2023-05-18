using RothschildHouse.Library.Client.DataContracts;
using RothschildHouse.Library.Client.DataContracts.Common.Contracts;

namespace RothschildHouse.Library.Client.Contracts
{
    public interface IRothschildHouseClient
    {
        Task<IListResponse<CountryItemModel>> SearchCountriesAsync(SearchCountriesQuery request);
    }
}
