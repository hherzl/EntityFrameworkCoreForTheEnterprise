using Microsoft.AspNetCore.Mvc;

namespace RothschildHouse.API.Notifications.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(ILogger<NotificationsController> logger)
        {
            _logger = logger;
        }
    }
}
