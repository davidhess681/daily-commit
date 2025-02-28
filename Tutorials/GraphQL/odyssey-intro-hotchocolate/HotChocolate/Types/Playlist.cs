using System.Threading.Tasks;
using SpotifyWeb;

namespace GraphQLServer.Types;

[GraphQLDescription("A curated collection of tracks designed for a specific activity or mood.")]
public class Playlist
{
    private List<Track>? _tracks;

    public Playlist(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public Playlist(PlaylistSimplified playlist)
    {
        Id = playlist.Id;
        Name = playlist.Name;
        Description = playlist.Description;
    }

    public Playlist(SpotifyWeb.Playlist playlist)
    {
        Id = playlist.Id;
        Name = playlist.Name;
        Description = playlist.Description;
        _tracks = playlist.Tracks.Items
            .Select(Map)
            .ToList();
    }

    [GraphQLDescription("The ID for the playlist.")]
    [ID]
    public string Id { get; }

    [GraphQLDescription("The name of the playlist.")]
    public string Name { get; set; }

    [GraphQLDescription("Describes the playlist, what to expect and entices the user to listen.")]
    public string? Description { get; set; }

    [GraphQLDescription("The playlist's tracks.")]
    public async Task<List<Track>> Tracks([Service] SpotifyService spotifyService)
    {
        if(_tracks is not null)
        {
            return _tracks;
        }
        var response = await spotifyService.GetPlaylistsTracksAsync(Id);

        return response.Items
            .Select(Map)
            .ToList();
    }

    private static Track Map(PlaylistTrack playlistTrack)
    {
        return new Track()
        {
            Id = playlistTrack.Track.Id,
            Name = playlistTrack.Track.Name,
            DurationMs = playlistTrack.Track.Duration_ms,
            Explicit = playlistTrack.Track.Explicit,
            Uri = playlistTrack.Track.Uri
        };
    }
}
