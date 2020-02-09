using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.API.Common.Controllers
{
#pragma warning disable CS1591
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : OnlineStoreController
    {
        public CommonController()
            : base()
        {
        }
    }
#pragma warning restore CS1591
}
