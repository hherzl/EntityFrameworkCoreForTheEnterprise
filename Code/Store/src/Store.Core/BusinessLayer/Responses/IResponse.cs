using System;

namespace Store.Core.BusinessLayer.Responses
{
    public interface IResponse
    {
        String Message { get; set; }

        Boolean DidError { get; set; }

        String ErrorMessage { get; set; }
    }
}
