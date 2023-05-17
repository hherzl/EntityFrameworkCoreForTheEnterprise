namespace RothschildHouse.Library.Common.Clients.DataContracts.Common
{
    public record Response
    {
        public Response()
        {
        }

        public Response(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
