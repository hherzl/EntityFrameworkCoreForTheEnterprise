using MediatR;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public class SearchCustomersQuery : IRequest<PagedResponse<CustomerItemModel>>
    {
        public SearchCustomersQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
