﻿namespace RothschildHouse.Mock.PaymentTransactions
{
    internal class Mocks
    {
        public static class ClientApplications
        {
            public static IEnumerable<Guid> Items
            {
                get
                {
                    // UCommerce
                    yield return Guid.Parse("9B26240D-6BFA-4F80-AE67-37712D1388A7");

                    // Mocks
                    yield return Guid.Parse("B74CB3C2-BB35-4436-BCFB-8769B521CA3D");

                    // GUI
                    yield return Guid.Parse("D4159097-96BE-43E0-9E8F-ED4384B0F9C2");
                }
            }
        }

        public static class Customers
        {
            public static IEnumerable<Guid> Items
            {
                get
                {
                    // Juan Carlos Pérez
                    yield return Guid.Parse("811A6254-F551-4FEB-AC9B-EC934E024BEC");

                    // María Rosales
                    yield return Guid.Parse("811A6254-F551-4FEB-AC9B-EC934E024BEC");

                    // Roberto Asturias
                    yield return Guid.Parse("811A6254-F551-4FEB-AC9B-EC934E024BEC");

                    // Julio Bonilla
                    yield return Guid.Parse("811A6254-F551-4FEB-AC9B-EC934E024BEC");
                }
            }
        }

        public static class Cards
        {
            public static IEnumerable<(short, string, string, string, string, string)> Items
            {
                get
                {
                    yield return (2000, "VISA", "Juan Carlos Pérez", "4111111111111111", "0124", "123");

                    yield return (2000, "VISA", "María Rosales", "4012888888881881", "0124", "123");

                    yield return (2000, "Mastercard", "Roberto Asturias", "5555555555554444", "0125", "456");

                    yield return (2000, "Mastercard", "Julio Bonilla", "5105105105105100", "0125", "456");
                }
            }
        }
    }
}