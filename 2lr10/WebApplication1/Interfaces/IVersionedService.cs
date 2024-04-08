using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Interfaces
{
    public interface IVersionedService
    {
        public int GetV1();
        public string GetV2();
        public IActionResult GetV3();
    }
}
