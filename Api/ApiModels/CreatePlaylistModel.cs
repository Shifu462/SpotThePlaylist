using Newtonsoft.Json;

namespace SpotThePlaylist.Web.Api.ApiModels
{
    public class CreatePlaylistModel
    {
        [JsonProperty("trackIds")]
        public string[] TrackIds { get; set; }

        [JsonProperty("playlistName")]
        public string PlaylistName { get; set; }
    }
}