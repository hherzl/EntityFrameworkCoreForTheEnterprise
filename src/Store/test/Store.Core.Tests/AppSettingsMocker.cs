using Store.Core.DataLayer;

namespace Store.Core.Tests
{
    public static class AppSettingsMocker
    {
        public static AppSettings Default
            => new AppSettings { ConnectionString = "server=(local);database=Store;integrated security=yes; " };
    }
}
