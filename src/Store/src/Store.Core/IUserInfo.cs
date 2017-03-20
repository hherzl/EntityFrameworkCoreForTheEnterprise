using System;

namespace Store.Core
{
    public interface IUserInfo
    {
        String Domain { get; set; }

        String Name { get; set; }

        String[] Roles { get; set; }
    }
}
