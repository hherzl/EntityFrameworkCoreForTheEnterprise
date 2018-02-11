namespace Store.Core
{
    public class UserInfo : IUserInfo
    {
        public UserInfo()
        {
        }

        public string Domain { get; set; }

        public string Name { get; set; }

        public string[] Roles { get; set; }
    }
}
