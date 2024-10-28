namespace RothschildHouse.Clients.CityBank;

internal class Database
{
    public List<Card> Cards { get; private set; }

    public void Init()
    {
        Cards = new()
        {
            new(2000, "VISA", "Juan Carlos Pérez", "4111111111111111", "0124", "111"),
            new(2000, "VISA", "María Rosales", "4012888888881881", "0624", "333"),
            new(2000, "Mastercard", "Julio Bonilla", "5105105105105100", "0125", "666"),
            new(2000, "Mastercard", "Roberto Asturias", "5555555555554444", "0325", "999"),
            new(2000, "American Express", "Amelia Brand", "371741686208810", "1224", "7015"),
            new(2000, "American Express", "Joseph Cooper", "371691232939410", "0624", "3708")
        };
    }
}
