namespace RothschildHouse.Clients.CityBank;

internal class Card
{
    public Card()
    {
    }

    public Card(short? cardTypeId, string issuingNetwork, string cardholderName, string cardNumber, string expirationDate, string cvv)
    {
        CardTypeId = cardTypeId;
        IssuingNetwork = issuingNetwork;
        CardholderName = cardholderName;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        Cvv = cvv;
    }

    public short? CardTypeId { get; set; }
    public string IssuingNetwork { get; set; }
    public string CardholderName { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string Cvv { get; set; }
}
