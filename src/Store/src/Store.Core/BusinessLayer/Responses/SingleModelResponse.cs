using System;

namespace Store.Core.BusinessLayer.Responses
{
    public class SingleModelResponse<TModel> : ISingleModelResponse<TModel> where TModel : new()
    {
        public SingleModelResponse()
        {
            Model = new TModel();
        }

        public String Message { get; set; }

        public Boolean DidError { get; set; }

        public String ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }
}
