using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class DiceController : Controller
    {
        [HttpGet("role-dice")]
        public IActionResult Index()
        {
            var value = new Random().Next(1, 7);
            using (var activity = Activity.Current)
            {
                if (activity != null)
                {
                    activity.SetTag("role.value", value);
                }
            }

            return Ok(value);
        }
    }
}
