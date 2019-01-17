using Microsoft.AspNetCore.Mvc;
using OnLineStore.Core;

namespace OnLineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    public class OnLineStoreController : ControllerBase
    {
        public OnLineStoreController()
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
