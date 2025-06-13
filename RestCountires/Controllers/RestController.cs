using RestCountries.Core.Interfaces;

namespace RestCountries.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly ILogger<RestController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly ISecondHighestService _secondHighestService;
        public RestController(ILogger<RestController> logger,IMemoryCache memoryCache, ISecondHighestService sndHighService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _secondHighestService = sndHighService;
        }

        [HttpPost("second")]
        public async Task<IActionResult> GetSecondHighest([FromBody] RequestObj req)
        {
            var res = await _secondHighestService.GetSecondHighest(req.RequestArrayObj);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            return Ok();
        }

    }
}
