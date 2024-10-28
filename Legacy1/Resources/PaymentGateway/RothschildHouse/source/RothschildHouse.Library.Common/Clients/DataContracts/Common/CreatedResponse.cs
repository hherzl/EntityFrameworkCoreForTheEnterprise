namespace RothschildHouse.Library.Common.Clients.DataContracts.Common
{
    public record CreatedResponse<TResult> : Response
    {
        public CreatedResponse()
        {
        }

        public CreatedResponse(TResult id)
        {
            Id = id;
        }

        public TResult Id { get; set; }
    }
}
