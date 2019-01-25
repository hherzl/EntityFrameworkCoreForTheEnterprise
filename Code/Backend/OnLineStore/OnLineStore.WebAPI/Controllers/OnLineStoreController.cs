using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core;

namespace OnlineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    public class OnlineStoreController : ControllerBase
    {
        public OnlineStoreController()
        {
        }

        private IUserInfo m_userInfo;

        public IUserInfo UserInfo
        {
            get
            {
                return m_userInfo ?? (m_userInfo = new UserInfo());
            }
            set
            {
                m_userInfo = value;
            }
        }
    }
#pragma warning restore CS1591
}
