using Microsoft.AspNetCore.Mvc;

namespace RothschildHouse.API.SearchEngine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchEngineController : ControllerBase
    {
        private readonly ILogger<SearchEngineController> _logger;

        public SearchEngineController(ILogger<SearchEngineController> logger)
        {
            _logger = logger;
        }
    }
}
