using System;

namespace Store.Core
{
    public class UserInfo : IUserInfo
    {
        public UserInfo()
        {
        }

        public String Domain { get; set; }

        public String Name { get; set; }

        public String[] Roles { get; set; }
    }
}
