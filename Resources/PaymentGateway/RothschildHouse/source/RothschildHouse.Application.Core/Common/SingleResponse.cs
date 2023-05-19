using RothschildHouse.Application.Core.Common.Contracts;

namespace RothschildHouse.Application.Core.Common
{
    public record SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public SingleResponse()
        {
        }

        public SingleResponse(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; set; }
    }
}
