using Store.Core.DataLayer;

namespace Store.Mocker
{
    public class AppSettingsMocker
    {
        public static AppSettings Default
            => new AppSettings
            {
                ConnectionString = "server=(local);database=Store;integrated security=yes;"
            };
    }
}
