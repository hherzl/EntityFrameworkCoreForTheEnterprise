namespace RothschildHouse.Mock.PaymentTransactions;

internal record MockSettings
{
    public MockSettings()
    {
        var now = DateTime.Now;
        StartDate = new DateTime(now.Year, 1, 1);
//        EndDate = new DateTime(now.Year, now.Month, now.Day);
        EndDate = new DateTime(now.Year, 12, 31);

        TransactionsPerDay = 5;
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TransactionsPerDay { get; set; }
}
