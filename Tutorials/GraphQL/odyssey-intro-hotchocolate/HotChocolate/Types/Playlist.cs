using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;
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

    // In a future refactor, I'd like to move this to some form of mapper so that these 
    // models are not tightly coupled like this.
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
    [Key]
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

    // I wonder what the typical convention is here, as far as having the Playlist
    // Reference Resolver function declared within the Playlist type vs elsewhere.
    [ReferenceResolver]
    public static async Task<Playlist?> GetPlaylistById(
        SpotifyService spotifyService,
        [Parent] Playlist playlist)
    {
        var response = await spotifyService.GetPlaylistAsync(playlist.Id);
        return new Playlist(response);
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
