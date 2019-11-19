using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SpotThePlaylist.Web.Extensions;

namespace SpotThePlaylist.Web.Api
{
    [Route("api/[controller]/[action]")]
    public class SpotifyController : Controller
    {
        public IActionResult IsTokenValid([FromQuery] string token)
        {
            return Ok();
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public async Task<IActionResult> GetRandomPic([FromQuery] string token)
        {
            using var http = new HttpClient();

            http.DefaultRequestHeaders.Add(
                "Authorization", "Bearer " + token);

            var resp = await http.GetAsync("https://api.spotify.com/v1/me/tracks?limit=50");

            if (resp.StatusCode != HttpStatusCode.OK) return BadRequest();

            var likedTracksJToken = JToken.Parse(await resp.Content.ReadAsStringAsync());

            var song = likedTracksJToken["items"].GetRandom()["track"];

            var artistId = song["artists"]
                            .Select(artist => artist["id"].ToString())
                            .GetRandom();

            var artistResp = await http.GetAsync("https://api.spotify.com/v1/artists/" + artistId);

            var artistJToken = JToken.Parse(await artistResp.Content.ReadAsStringAsync());
            var url = artistJToken["images"]
                            .OrderByDescending(img => img["width"])
                            .ThenByDescending(img => img["height"])
                            .FirstOrDefault()
                            ?["url"].ToString();

            return Ok(
                new { backgroundUrl = url });
        }
    }
}