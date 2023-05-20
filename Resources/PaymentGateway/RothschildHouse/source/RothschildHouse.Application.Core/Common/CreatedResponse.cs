﻿namespace RothschildHouse.Application.Core.Common
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
