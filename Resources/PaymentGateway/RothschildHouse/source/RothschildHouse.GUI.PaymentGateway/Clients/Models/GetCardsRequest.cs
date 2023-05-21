﻿namespace RothschildHouse.GUI.PaymentGateway.Clients.Models
{
    public record GetCardsRequest
    {
        public GetCardsRequest()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}