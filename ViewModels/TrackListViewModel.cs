using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyAPI.Web.Models;

namespace SpotThePlaylist.Web.ViewModels
{
    public class TrackListViewModel
    {
        public ICollection<TrackViewModel> Tracks { get; set; }
    }

    public class TrackViewModel
    {
        public string Id { get; private set; }
        public string Author { get; private set; }
        public string Name { get; private set; }
        public string Album { get; private set; }
        public DateTime? PlayedAt { get; private set; }

        public static TrackViewModel FromSimpleTrack(SimpleTrack track, DateTime? playedAt = null)
        {
            var authorNames = track.Artists.Select(a => a.Name);

            return new TrackViewModel
            {
                Id = track.Id,
                Name = track.Name,
                Author = string.Join(", ", authorNames),
                Album = track.Type, /// а где альбом
                PlayedAt = playedAt,
            };
        }
    }
}