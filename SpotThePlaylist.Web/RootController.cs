using Microsoft.AspNetCore.Mvc;

namespace SpotThePlaylist.Web
{
    public class RootController : Controller
    {
        public IActionResult Root()
        {
            return this.View("~/wwwroot/index.html");
        }
    }
}
