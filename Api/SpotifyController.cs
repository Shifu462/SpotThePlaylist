using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SpotifyAPI.Web;
using SpotThePlaylist.Web.Api.ApiModels;
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

        public async Task<IActionResult> GetNewPlaylist([FromQuery] string token)
        {
            var api = new SpotifyWebAPI
            {
                AccessToken = token,
                TokenType = "Bearer",
            };

            var recentTracks = await api.GetUsersRecentlyPlayedTracksAsync(50);

            if (recentTracks.HasError()) return BadRequest(recentTracks.Error.Message);

            var recentTracksIds = recentTracks.Items.Select(x => x.Track.Id).ToArray();

            var recentTracksAudioFeatures = await api.GetSeveralAudioFeaturesAsync(
                recentTracksIds.ToList() // почините либу плиз
            );

            // var tuneableTrackSum =
            //     recentTracksAudioFeatures.AudioFeatures
            //         .Aggregate(
            //             new SpotifyAPI.Web.Models.TuneableTrack
            //             {
            //                 Tempo = 0,
            //                 Speechiness = 0,
            //                 Loudness = 0,
            //                 Liveness = 0,
            //                 Instrumentalness = 0,
            //                 Energy = 0,
            //                 DurationMs = 0,
            //                 Danceability = 0,
            //                 Acousticness = 0,
            //                 Key = 0,
            //                 Valence = 0,
            //             },
            //         (tuneableTrack, feature) =>
            //         {
            //             tuneableTrack.Tempo += feature.Tempo;
            //             tuneableTrack.Speechiness += feature.Speechiness;
            //             tuneableTrack.Loudness += feature.Loudness;
            //             tuneableTrack.Liveness += feature.Liveness;
            //             tuneableTrack.Instrumentalness += feature.Instrumentalness;
            //             tuneableTrack.Energy += feature.Energy;
            //             tuneableTrack.DurationMs += feature.DurationMs;
            //             tuneableTrack.Danceability += feature.Danceability;
            //             tuneableTrack.Acousticness += feature.Acousticness;
            //             tuneableTrack.Key += feature.Key;
            //             tuneableTrack.Valence += feature.Valence;

            //             return tuneableTrack;
            //         });

            // tuneableTrackSum.Tempo /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Speechiness /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Loudness /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Liveness /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Instrumentalness /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Energy /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.DurationMs /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Danceability /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Acousticness /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Key /= recentTracksAudioFeatures.AudioFeatures.Count;
            // tuneableTrackSum.Valence /= recentTracksAudioFeatures.AudioFeatures.Count;

            var tracks = await api.GetRecommendationsAsync(trackSeed: recentTracksIds.Take(5).ToList());

            var tracksModel =
                tracks.Tracks.Select(x => TrackViewModel.FromSimpleTrack(x))
                             .ToArray();

            return Ok(new TrackListViewModel { Tracks = tracksModel });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromQuery] string token,
            [FromBody] CreatePlaylistModel data)
        {
            var api = new SpotifyWebAPI
            {
                AccessToken = token,
                TokenType = "Bearer",
            };

            var headers = new Dictionary<string, string> {
                    { "Authorization", "Bearer " + token }
                };

            var me = await api.WebClient.DownloadAsync("https://api.spotify.com/v1/me", headers);

            var meJson = JToken.Parse(me.Item2);

            var userId = meJson["id"].Value<string>();


            var playlistName = data.PlaylistName
                             ?? "Spot " + DateTime.UtcNow.ToString("o");

            var playlist = await api.CreatePlaylistAsync(userId, playlistName, isPublic: false);

            var trackUris = data.TrackIds.Select(x => "spotify:track:" + x).ToList();
            var addPlaylistTracksResp = await api.AddPlaylistTracksAsync(playlist.Id, trackUris);

            return Ok(playlist.Id);
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

            var tracksModel = recentTracks.Items
                                .Select(x => TrackViewModel.FromSimpleTrack(x.Track, x.PlayedAt))
                                .ToArray();

            return Ok(new TrackListViewModel { Tracks = tracksModel });
        }

        public async Task<IActionResult> GetRandomPicture([FromQuery] string token)
        {
            var api = new SpotifyWebAPI
            {
                AccessToken = token,
                TokenType = "Bearer",
            };

            var savedTracks = await api.GetSavedTracksAsync(50, market: "US");

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