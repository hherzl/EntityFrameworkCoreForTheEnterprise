using System.Text;
using System.Text.Json;
using MediatR;
using RothschildHouse.Library.Client.DataContracts.Common.Contracts;

namespace RothschildHouse.Library.Client.DataContracts
{
    public class SearchCountriesQuery : IRequest<IListResponse<CountryItemModel>>
    {
        public SearchCountriesQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);

        public virtual StringContent ToStringContent(string mediaType)
            => new(ToJson(), Encoding.Default, mediaType);
    }
}
