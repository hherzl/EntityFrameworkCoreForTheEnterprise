﻿namespace OnlineStore.Core.Business
{
    public static class SalesDisplays
    {
        public static string NonExistingProductExceptionMessage
            => "Sent order has a non existing product with ID: '{0}', order has been rejected.";

        public static string AddOrderWithDiscontinuedProductExceptionMessage
            => "Product with ID: '{0}' is discontinued, order has been rejected.";

        public static string InvalidQuantityExceptionMessage
            => "Sent order has an invalid quantity for product with ID: '{0}', order has been rejected.";

        public static string RemoveOrderExceptionMessage
            => "Order with ID: {0} cannot be deleted, because has dependencies. Please contact to technical support for more details";

        public static string CreateOrderMessage
            => "Order was created successfully";

        public static string DeleteOrderMessage
            => "Order was deleted successfully";
    }
}
