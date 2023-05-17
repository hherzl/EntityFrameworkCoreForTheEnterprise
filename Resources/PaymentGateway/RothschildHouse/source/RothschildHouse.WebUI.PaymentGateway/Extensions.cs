namespace RothschildHouse.WebUI.PaymentGateway
{
    public static class Extensions
    {
        public static string ToDateTimeString(this DateTime? dateTime)
            => dateTime.HasValue ? dateTime.Value.ToString("MM/dd/yyyy hh:mm tt") : "";
    }
}
