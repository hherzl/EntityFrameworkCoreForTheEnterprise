using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core;

namespace OnlineStore.API.Common.Controllers
{
    public class OnlineStoreController : ControllerBase
    {
        private IUserInfo m_userInfo;

        public OnlineStoreController()
        {
        }

        public IUserInfo UserInfo
        {
            get => m_userInfo ?? (m_userInfo = new UserInfo());
            set => m_userInfo = value;
        }
    }
}
