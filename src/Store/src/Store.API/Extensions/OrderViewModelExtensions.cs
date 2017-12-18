﻿using System.Collections.Generic;
using Store.API.RequestModels;
using Store.Core.EntityLayer.Sales;

namespace Store.API.Extensions
{
    public static class OrderViewModelExtensions
    {
        public static Order GetOrder(this OrderViewModel requestModel)
        {
            return new Order
            {
                OrderID = requestModel.OrderID,
                OrderDate = requestModel.OrderDate,
                CustomerID = requestModel.CustomerID,
                EmployeeID = requestModel.EmployeeID,
                ShipperID = requestModel.ShipperID,
                Total = requestModel.Total,
                Comments = requestModel.Comments,
                CreationUser = requestModel.CreationUser,
                CreationDateTime = requestModel.CreationDateTime,
                LastUpdateUser = requestModel.LastUpdateUser,
                LastUpdateDateTime = requestModel.LastUpdateDateTime
            };
        }

        public static IEnumerable<OrderDetail> GetOrderDetails(this OrderViewModel requestModel)
        {
            foreach (var item in requestModel.Details)
            {
                yield return new OrderDetail
                {
                    OrderID = item.OrderID,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total
                };
            }
        }
    }
}
