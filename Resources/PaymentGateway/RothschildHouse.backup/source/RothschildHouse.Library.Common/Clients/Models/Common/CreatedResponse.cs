namespace RothschildHouse.Library.Common.Clients.Models.Common
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
