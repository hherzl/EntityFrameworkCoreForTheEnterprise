using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.API.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : OnlineStoreController
    {
        public CommonController()
            : base()
        {
        }
    }
}
