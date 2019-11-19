using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotThePlaylist.Web.Extensions;
using SpotThePlaylist.Web.ViewModels;

namespace SpotThePlaylist.Web.Api
{
    [Route("api/[controller]/[action]")]
    public class SpotifyController : Controller
    {
        public IActionResult IsTokenValid([FromQuery] string token)
        {
            return Ok();
        }

        public async Task<IActionResult> GetRecent([FromQuery] string token)
        {
            var api = new SpotifyWebAPI
            {
                AccessToken = token,
                TokenType = "Bearer",
            };

            var recentTracks = await api.GetUsersRecentlyPlayedTracksAsync(50);

            if (recentTracks.HasError()) return BadRequest(recentTracks.Error.Message);
            
            var tracks = recentTracks.Items
                                .Select(x => TrackViewModel.FromSimpleTrack(x.Track, x.PlayedAt))
                                .ToArray();

            return Ok(new TrackListViewModel { Tracks = tracks });
        }

        public async Task<IActionResult> GetRandomPicture([FromQuery] string token)
        {
            var api = new SpotifyWebAPI
            {
                AccessToken = token,
                TokenType = "Bearer",
            };

            var savedTracks = await api.GetSavedTracksAsync(50, market: "RU");

            if (savedTracks.HasError()) return BadRequest(savedTracks.Error.Message);

            var song = savedTracks.Items.GetRandom().Track;

            var artist = song.Artists.GetRandom();
            var artistFull = await api.GetArtistAsync(artist.Id);

            var biggestImage = artistFull.Images
                             .OrderByDescending(img => img.Width)
                             .ThenByDescending(img => img.Height)
                             .FirstOrDefault();

            var biggestImageUrl = biggestImage?.Url;

            return Ok(
                new { backgroundUrl = biggestImageUrl });
        }
    }
}