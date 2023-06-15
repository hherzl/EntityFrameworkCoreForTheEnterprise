namespace RothschildHouse.GUI.PaymentGateway
{
    public static class Extensions
    {
        public static double[] ToDoubleArray(this IEnumerable<decimal> sequence)
            => sequence.Select(item => (double)item).ToArray();
    }
}
