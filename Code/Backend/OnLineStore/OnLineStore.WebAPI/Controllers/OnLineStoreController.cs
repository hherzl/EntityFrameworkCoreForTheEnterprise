using Microsoft.AspNetCore.Mvc;
using OnLineStore.Core;

namespace OnLineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    public class OnLineStoreController : ControllerBase
    {
        public IUserInfo UserInfo { get; set; }
    }
#pragma warning restore CS1591
}
