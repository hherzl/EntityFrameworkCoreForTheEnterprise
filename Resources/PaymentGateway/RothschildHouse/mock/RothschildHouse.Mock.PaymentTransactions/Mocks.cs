namespace RothschildHouse.Mock.PaymentTransactions
{
    internal class Mocks
    {
        public static class ClientApplications
        {
            public static IEnumerable<Guid> Items
            {
                get
                {
                    // eCommerce
                    yield return Guid.Parse("9B26240D-6BFA-4F80-AE67-37712D1388A7");

                    // Mocks
                    yield return Guid.Parse("B74CB3C2-BB35-4436-BCFB-8769B521CA3D");

                    // PayButton
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
                    yield return Guid.Parse("2341BD82-0235-4233-A103-24CC8D83A51A");

                    // Roberto Asturias
                    yield return Guid.Parse("24868311-7D35-46B3-A6C7-57D10DE064C4");

                    // Julio Bonilla
                    yield return Guid.Parse("F2E95CCA-6E96-456C-92B1-E016990458FF");

                    // Amelia Brand
                    yield return Guid.Parse("4B76BB9C-BF57-43C7-B75E-13EB0C047F47");

                    // Joseph Cooper
                    yield return Guid.Parse("EE3EE5D7-40A1-4AA8-9B8D-B596B6B8C575");
                }
            }
        }

        public static class Cards
        {
            public static IEnumerable<(short, string, string, string, string, string)> Items
            {
                get
                {
                    yield return (2000, "VISA", "Juan Carlos Pérez", "4111111111111111", "0124", "111");

                    yield return (2000, "VISA", "María Rosales", "4012888888881881", "0624", "333");

                    yield return (2000, "Mastercard", "Julio Bonilla", "5105105105105100", "0125", "666");

                    yield return (2000, "Mastercard", "Roberto Asturias", "5555555555554444", "0325", "999");

                    yield return (2000, "American Express", "Amelia Brand", "371741686208810", "1224", "7015");

                    yield return (2000, "American Express", "Joseph Cooper", "371691232939410", "0624", "3708");
                }
            }
        }

        public static class Currencies
        {
            public static IEnumerable<string> Items
            {
                get
                {
                    yield return "USD";
                }
            }
        }
    }
}
