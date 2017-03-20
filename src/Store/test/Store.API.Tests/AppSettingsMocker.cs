using Store.Core.DataLayer;

namespace Store.API.Tests
{
    public static class AppSettingsMocker
    {
        public static AppSettings Default
        {
            get
            {
                return new AppSettings
                {
                    ConnectionString = "server=(local);database=Store;integrated security=yes; "
                };
            }
        }
    }
}
