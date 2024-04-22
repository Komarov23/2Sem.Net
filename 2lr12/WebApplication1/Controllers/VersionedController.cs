using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/versioned")]
    [Authorize]
    public class VersionedController(IVersionedService versionedService) : ControllerBase
    {
        private IVersionedService _versionedService = versionedService;

        [HttpGet]
        public int GetV1() => _versionedService.GetV1();

        [HttpGet, MapToApiVersion("2.0")]
        [ApiExplorerSettings(GroupName = "v2.0")]
        public string GetV2() => _versionedService.GetV2();

        [HttpGet, MapToApiVersion("3.0")]
        [ApiExplorerSettings(GroupName = "v3.0")]
        public IActionResult GetV3() => _versionedService.GetV3();
    }
}
