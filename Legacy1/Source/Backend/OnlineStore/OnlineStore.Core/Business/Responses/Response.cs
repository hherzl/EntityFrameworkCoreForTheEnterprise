namespace OnlineStore.Core.Business.Responses
{
    public class Response : IResponse
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }
    }
}
