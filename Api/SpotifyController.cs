using Microsoft.AspNetCore.Mvc;

namespace SpotThePlaylist.Web.Api
{
    [Route("api/[controller]/[action]")]
    public class SpotifyController : Controller
    {
        public IActionResult IsTokenValid([FromQuery] string token)
        {
            return Ok();
        }
    }
}