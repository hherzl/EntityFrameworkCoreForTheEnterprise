using System;
using System.Collections.Generic;

namespace Store.Core.BusinessLayer.Responses
{
    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public String Message { get; set; }

        public Boolean DidError { get; set; }

        public String ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }
}
