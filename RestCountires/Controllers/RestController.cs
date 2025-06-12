namespace RestCountries.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly ILogger<RestController> _logger;
        private readonly IMemoryCache _memoryCache;
        public RestController(ILogger<RestController> logger,IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpPost("second")]
        public async Task<IActionResult> GetSecondHighest([FromBody] RequestObj req)
        {
            return Ok();
        }
    }
}
