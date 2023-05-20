namespace RothschildHouse.TP.CityBank.Data
{
    internal class Database
    {
        public List<Card> Cards { get; private set; }

        public void Init()
        {
            Cards = new();

            Cards.Add(new Card(2000, "VISA", "Juan Carlos Pérez", "4111111111111111", "0124", "123"));
            Cards.Add(new Card(2000, "VISA", "María Rosales", "4012888888881881", "0124", "123"));
            Cards.Add(new Card(2000, "Mastercard", "Roberto Asturias", "5555555555554444", "0125", "456"));
            Cards.Add(new Card(2000, "Mastercard", "Julio Bonilla", "5105105105105100", "0125", "456"));
        }
    }
}
