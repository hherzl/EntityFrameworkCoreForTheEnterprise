using System.Globalization;

namespace RothschildHouse.WebUI.PaymentGateway
{
    public record SupportedCultures
    {
        public static string[] Items
            => new[] { "en-US", "es-SV", "he-IL" };

        public static CultureInfo[] CultureInfos
            => Items.Select(item => new CultureInfo(item)).ToArray();
    }
}
